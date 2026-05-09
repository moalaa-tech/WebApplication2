using AutoMapper;
using CRM.Domain.Entities.InventoryManagement;
using CRM.Domain.Enums;
using CRM.WebApp.DTOs.InventoryManagement;
using CRM.WebApp.DTOs.InventoryManagement.Product;
using CRM.WebApp.DTOs.Order;
using CRM.WebApp.Paging;
using CRM.WebApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public InventoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateItemAsync(ProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _uow.Products.AddAsync(entity);
            await _uow.CompleteAsync();
            return _mapper.Map<ProductDto>(entity);
        }

        public async Task<PaginatedList<ProductDto>> GetItemsAsync(int pageNumber = 1, int pageSize = 25)
        {
            var query = _uow.Products.Query();
            query = query.Select(a => new Product
            {
                Id=a.Id,
                Name=a.Name,
                NameAr=a.NameAr,
                TotalCost = a.TotalCost,
                
            });



            var mn = query.ToQueryString();
            //var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var TotalCount = query.Count();
            var result = await PaginatedList<ProductDto>.CreateAsync<Product, ProductDto>(query, pageNumber, pageSize, _mapper.ConfigurationProvider);
            result.TotalCount = TotalCount;
            return result;
        }

        public async Task ReceiveGoodsAsync(StockTransactionDto dto)
        {
            int? batchId = null;

            if (!string.IsNullOrWhiteSpace(dto.BatchNumber) || dto.ExpiryDate.HasValue)
            {
                // Create a new batch on receipt
                var batch = new Batch
                {
                    BatchNumber = string.IsNullOrWhiteSpace(dto.BatchNumber) ? Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper() : dto.BatchNumber!,
                    ManufactureDate = dto.ManufactureDate,
                    ExpiryDate = dto.ExpiryDate,
                    ProductId = dto.ItemId,
                    WarehouseId = dto.WarehouseId,
                    InitialQuantity = dto.Quantity,
                    QuantityOnHand = dto.Quantity
                };
                await _uow.Repository<Batch>().AddAsync(batch);
                await _uow.CompleteAsync();
                batchId = batch.Id;
            }

            if (!string.IsNullOrWhiteSpace(dto.Barcode))
            {
                var existing = await _uow.Repository<ProductBarcode>().GetAsync(b => b.Code == dto.Barcode);
                if (existing == null)
                {
                    var pb = new ProductBarcode { Code = dto.Barcode!, Symbology = null, IsPrimary = false, ProductId = dto.ItemId };
                    await _uow.Repository<ProductBarcode>().AddAsync(pb);
                    await _uow.CompleteAsync();
                }
            }

            var tx = new StockTransaction
            {
                ItemId = dto.ItemId,
                Item = null!,
                WarehouseId = dto.WarehouseId,
                Warehouse = null!,
                TransactionType = TransactionType.In,
                Quantity = dto.Quantity,
                TransactionDate = DateTime.UtcNow,
                Reference = dto.Reference,
                Note = dto.Note,
                BatchId = batchId,
                ScannedBarcode = dto.Barcode
            };

            await _uow.StockTransactions.AddAsync(tx);
            await _uow.CompleteAsync();
        }

        public async Task IssueGoodsAsync(StockTransactionDto dto)
        {
            decimal remaining = dto.Quantity;
            // FEFO: earliest expiry first, null expiry last
            IQueryable<Batch> batchesQ = _uow.Repository<Batch>().Query()
                .Where(b => b.ProductId == dto.ItemId && b.WarehouseId == dto.WarehouseId && b.QuantityOnHand > 0);

            if (!string.IsNullOrWhiteSpace(dto.BatchNumber))
            {
                batchesQ = batchesQ.Where(b => b.BatchNumber == dto.BatchNumber);
            }

            var batches = await batchesQ
                .OrderBy(b => b.ExpiryDate.HasValue ? 0 : 1)
                .ThenBy(b => b.ExpiryDate)
                .ToListAsync();
            foreach (var batch in batches)
            {
                if (remaining <= 0) break;
                var take = Math.Min(remaining, batch.QuantityOnHand);
                batch.QuantityOnHand -= take;

                var tx = new StockTransaction
                {
                    ItemId = dto.ItemId,
                    Item = null!,
                    WarehouseId = dto.WarehouseId,
                    Warehouse = null!,
                    TransactionType = TransactionType.Out,
                    Quantity = take,
                    TransactionDate = DateTime.UtcNow,
                    Reference = dto.Reference,
                    Note = dto.Note,
                    BatchId = batch.Id,
                    ScannedBarcode = dto.Barcode
                };
                await _uow.StockTransactions.AddAsync(tx);
                remaining -= take;
            }

            if (remaining > 0)
            {
                throw new InvalidOperationException("Insufficient stock across available batches.");
            }

            await _uow.CompleteAsync();
        }
        
        public async Task<int> GetCurrentStockAsync(int itemId, int? warehouseId = null)
        {
            var query = _uow.StockTransactions.Query()
                .Where(t => t.ItemId == itemId);
                
            if (warehouseId.HasValue)
            {
                query = query.Where(t => t.WarehouseId == warehouseId.Value);
            }
            
            var inStock = await query.Where(t => t.TransactionType == TransactionType.In)
                .SumAsync(t => t.Quantity);
                
            var outStock = await query.Where(t => t.TransactionType == TransactionType.Out)
                .SumAsync(t => t.Quantity);
            var adjustmentStock = (inStock - outStock);
            return  Convert.ToInt32(adjustmentStock);
        }
        
        public async Task<IEnumerable<ProductDto>> GetReorderSuggestionsAsync()
        {
            var products = await _uow.Products.Query()
                .Include(p => p.ReorderRule)
                .Where(p => p.ReorderRule != null)
                .ToListAsync();
                
            var result = new List<ProductDto>();
            
            foreach (var product in products)
            {
                var currentStock = await GetCurrentStockAsync(product.Id);
                if (product.ReorderRule != null && currentStock <= product.ReorderRule.MinimumQuantity)
                {
                    result.Add(_mapper.Map<ProductDto>(product));
                }
            }
            
            return result;
        }
        
        public async Task<IEnumerable<BatchDto>> GetBatchesForItemAsync(int itemId, int? warehouseId = null)
        {
            var query = _uow.Repository<Batch>().Query()
                .Where(b => b.ProductId == itemId && b.QuantityOnHand > 0);
                
            if (warehouseId.HasValue)
            {
                query = query.Where(b => b.WarehouseId == warehouseId.Value);
            }
            
            var batches = await query
                .OrderBy(b => b.ExpiryDate.HasValue ? 0 : 1)
                .ThenBy(b => b.ExpiryDate)
                .ToListAsync();
                
            return _mapper.Map<IEnumerable<BatchDto>>(batches);
        }
        
        public async Task<ProductDto?> LookupProductByBarcodeAsync(string code)
        {
            var barcode = await _uow.Repository<ProductBarcode>().Query()
                .Include(b => b.Product)
                .FirstOrDefaultAsync(b => b.BarcodeValue == code);
                
            if (barcode == null)
                return null;
                
            return _mapper.Map<ProductDto>(barcode.Product);
        }
        
        public async Task<IEnumerable<WarehouseDto>> GetWarehousesAsync()
        {
            var warehouses = await _uow.Repository<Warehouse>().Query()
                .OrderBy(w => w.Name)
                .ToListAsync();
                
            return _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
        }

        //public async Task<decimal> GetCurrentStockAsync(int itemId, int? warehouseId = null)
        //{
        //    var q = _uow.StockTransactions.Query().Where(s => s.ItemId == itemId);
        //    if (warehouseId.HasValue) q = q.Where(s => s.WarehouseId == warehouseId.Value);

        //    var ins = await q.Where(x => x.TransactionType == TransactionType.In).SumAsync(x => (decimal?)x.Quantity) ?? 0m;
        //    var outs = await q.Where(x => x.TransactionType == TransactionType.Out).SumAsync(x => (decimal?)x.Quantity) ?? 0m;
        //    var adjustments = await q.Where(x => x.TransactionType == TransactionType.Adjustment).SumAsync(x => (decimal?)x.Quantity) ?? 0m; // adjustments may be +/-

        //    return ins - outs + adjustments;
        //}

        //public async Task<IEnumerable<ProductDto>> GetReorderSuggestionsAsync()
        //{
        //    var items = _uow.Products.Query().Include(i => i.ReorderRule).ToList();
        //    var suggestions = new List<ProductDto>();
        //    foreach (var item in items)
        //    {
        //        if (item.ReorderRule == null) continue;
        //        var current = await GetCurrentStockAsync(item.Id);
        //        if (current <= item.ReorderRule.MinQty)
        //        {
        //            suggestions.Add(_mapper.Map<ProductDto>(item));
        //        }
        //    }
        //    return suggestions;
        //}

        //public async Task<IEnumerable<BatchDto>> GetBatchesForItemAsync(int itemId, int? warehouseId = null)
        //{
        //    var q = _uow.Repository<Batch>().Query().Where(b => b.ProductId == itemId);
        //    if (warehouseId.HasValue) q = q.Where(b => b.WarehouseId == warehouseId.Value);
        //    var list = await q.OrderBy(b => b.ExpiryDate).ToListAsync();
        //    return _mapper.Map<IEnumerable<BatchDto>>(list);
        //}

        //public async Task<ProductDto?> LookupProductByBarcodeAsync(string code)
        //{
        //    var barcode = await _uow.Repository<ProductBarcode>().GetAsync(b => b.Code == code,
        //        includes: q => q.Include(x => x.Product));
        //    return barcode == null ? null : _mapper.Map<ProductDto>(barcode.Product);
        //}
    }
}
