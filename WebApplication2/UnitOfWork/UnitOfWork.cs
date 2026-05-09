using CRM.Domain.Base;
using CRM.Domain.Entities.InventoryManagement;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DbContext;
using CRM.WebApp.Repositories;

namespace CRM.WebApp.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public IRepository<Warehouse> Warehouses { get; }
        public IRepository<StockTransaction> StockTransactions { get; }
        public IRepository<ReorderRule> ReorderRules { get; }

        public IRepository<Product> Products { get; set; }
        public IRepository<Batch> Batches { get; set; }
        public IRepository<ProductBarcode> ProductBarcodes { get; set; }

        public IRepository<Campaign> Campaigns { get; set; }
        public IRepository<EmailTemplate> EmailTemplates { get; set; }

        public UnitOfWork(
            ApplicationContext context, 
            IRepository<Campaign> _Campaigns, 
            IRepository<EmailTemplate> _emailTemplates,
            IRepository<Product> _Products,
            IRepository<Warehouse> _Warehouses,
            IRepository<StockTransaction> _StockTransactions,
            IRepository<ReorderRule> _ReorderRules,
            IRepository<Batch> _Batches,
            IRepository<ProductBarcode> _ProductBarcodes


            )
        {
            _context = context;
            Campaigns = _Campaigns;
            EmailTemplates= _emailTemplates;
            Products = _Products;
            Warehouses = _Warehouses;
            StockTransactions = _StockTransactions;
            ReorderRules = _ReorderRules;
            Batches = _Batches;
            ProductBarcodes = _ProductBarcodes;
        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
