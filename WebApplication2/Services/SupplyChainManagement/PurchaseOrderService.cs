using AutoMapper;
using CRM.Domain.Entities.SupplyChainManagement;
using CRM.WebApp.DTOs.SupplyChainManagement.Procurement;
using CRM.WebApp.DTOs.SupplyChainManagement.Purchase;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.SupplyChainManagement
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IRepository<PurchaseOrder> PurchaseOrderRepository;
        private readonly IRepository<PurchaseOrderItem> PurchaseOrderItemRepository;

        private readonly IMapper _mapper;

        public PurchaseOrderService(
            IRepository<PurchaseOrder> _PurchaseOrderRepository, 
            IRepository<PurchaseOrderItem> _PurchaseOrderItemRepository,
            IMapper mapper)
        {
            PurchaseOrderRepository = _PurchaseOrderRepository;
            _mapper = mapper;
            PurchaseOrderItemRepository = _PurchaseOrderItemRepository;
        }

        public async Task<IEnumerable<PurchaseOrderDto>> GetAllPurchaseOrdersAsync()
        {
            var purchaseOrders = await PurchaseOrderRepository.GetAll()
                .Include(po => po.Supplier)
                .Include(po => po.Items)
                .OrderByDescending(po => po.OrderDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PurchaseOrderDto>>(purchaseOrders);
        }

        public async Task<PurchaseOrderDto> GetPurchaseOrderByIdAsync(int id)
        {
            var purchaseOrder = await PurchaseOrderRepository.GetAll()
                .Include(po => po.Supplier)
                .Include(po => po.Items)
                .FirstOrDefaultAsync(po => po.Id == id);

            return _mapper.Map<PurchaseOrderDto>(purchaseOrder);
        }

        public async Task<PurchaseOrderDto> CreatePurchaseOrderAsync(CreatePurchaseOrderDto createPurchaseOrderDto)
        {
            // Check if PO Number already exists
            if (await PONumberExistsAsync(createPurchaseOrderDto.PONumber))
            {
                throw new ArgumentException("PO Number already exists.");
            }

            var purchaseOrder = _mapper.Map<PurchaseOrder>(createPurchaseOrderDto);

            // Calculate total amount
            purchaseOrder.TotalAmount = purchaseOrder.Items?.Sum(item => item.Quantity * item.UnitPrice) ?? 0;

          await  PurchaseOrderRepository.AddAsync(purchaseOrder);
            await PurchaseOrderRepository.SaveChangesAsync();

            // Reload with related data
            var createdOrder = await PurchaseOrderRepository.GetAll()
                .Include(po => po.Supplier)
                .Include(po => po.Items)
                .FirstOrDefaultAsync(po => po.Id == purchaseOrder.Id);

            return _mapper.Map<PurchaseOrderDto>(createdOrder);
        }

        public async Task UpdatePurchaseOrderAsync(UpdatePurchaseOrderDto updatePurchaseOrderDto)
        {
            // Check if PO Number already exists (excluding current order)
            if (await PONumberExistsAsync(updatePurchaseOrderDto.PONumber, updatePurchaseOrderDto.Id))
            {
                throw new ArgumentException("PO Number already exists.");
            }

            var purchaseOrder = await PurchaseOrderRepository.GetAll()
                .Include(po => po.Items)
                .FirstOrDefaultAsync(po => po.Id == updatePurchaseOrderDto.Id);

            if (purchaseOrder == null)
                throw new ArgumentException("Purchase Order not found");

            // Remove existing items
            var existingItems = purchaseOrder.Items.ToList();
            PurchaseOrderItemRepository.RemoveRange(existingItems);

            // Update main properties
            _mapper.Map(updatePurchaseOrderDto, purchaseOrder);

            // Add new items
            var newItems = _mapper.Map<List<PurchaseOrderItem>>(updatePurchaseOrderDto.Items);
            foreach (var item in newItems)
            {
                purchaseOrder.Items.Add(item);
            }

            // Recalculate total amount
            purchaseOrder.TotalAmount = purchaseOrder.Items.Sum(item => item.Quantity * item.UnitPrice);
            purchaseOrder.DateModified = DateTime.UtcNow;

            PurchaseOrderRepository.Update(purchaseOrder);
            await PurchaseOrderRepository.SaveChangesAsync();
        }

        public async Task DeletePurchaseOrderAsync(int id)
        {
            var purchaseOrder = await PurchaseOrderRepository.GetByIdAsync(id);
            if (purchaseOrder != null)
            {
                PurchaseOrderRepository.Remove(purchaseOrder);
                await PurchaseOrderRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> PurchaseOrderExistsAsync(int id)
        {
            return await PurchaseOrderRepository.GetAll().AnyAsync(e => e.Id == id);
        }

        public async Task<bool> PONumberExistsAsync(string poNumber, int? excludeId = null)
        {
            if (excludeId.HasValue)
            {
                return await PurchaseOrderRepository.GetAll()
                    .AnyAsync(po => po.PONumber == poNumber && po.Id != excludeId.Value);
            }

            return await PurchaseOrderRepository.GetAll().AnyAsync(po => po.PONumber == poNumber);
        }

        public async Task ChangeStatusAsync(int id, string status)
        {
            var purchaseOrder = await PurchaseOrderRepository.GetByIdAsync(id);
            if (purchaseOrder == null)
                throw new ArgumentException("Purchase Order not found");

            purchaseOrder.Status = status;
            purchaseOrder.DateModified = DateTime.UtcNow;

            PurchaseOrderRepository.Update(purchaseOrder);
            await PurchaseOrderRepository.SaveChangesAsync();
        }

    
    }
}
