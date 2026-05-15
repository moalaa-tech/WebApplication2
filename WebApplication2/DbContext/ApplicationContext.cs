using CRM.Domain.Entities;
using CRM.Domain.Entities.Accounting;
using CRM.Domain.Entities.AccountsPayable;
using CRM.Domain.Entities.AccountsReceivable;
using CRM.Domain.Entities.AssetsManagment;
using CRM.Domain.Entities.Banking;
using CRM.Domain.Entities.Budgeting;
using CRM.Domain.Entities.CustomerService;
using CRM.Domain.Entities.DocumentManagement;
using CRM.Domain.Entities.HR;
using CRM.Domain.Entities.InventoryManagement;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.Domain.Entities.MarketingAutomation.EasyOrder;
using CRM.Domain.Entities.ProjectManagment;
using CRM.Domain.Entities.SalesManagement;
using CRM.Domain.Entities.SupplyChainManagement;
using CRM.Domain.IdentityEntity;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CRM.WebApp.DbContext
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>,IDataProtectionKeyContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<CRM.Domain.Entities.AccountsReceivable.Invoice>().ToTable("ARInvoices");
            builder.Entity<CRM.Domain.Entities.AccountsPayable.Invoice>().ToTable("APInvoices");

            builder.Entity<CRM.Domain.Entities.AccountsReceivable.InvoiceLine>().ToTable("ARInvoiceLine");
            builder.Entity<CRM.Domain.Entities.AccountsPayable.InvoiceLine>().ToTable("APInvoiceLine");
            base.OnModelCreating(builder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.EnableSensitiveDataLogging();
            //optionsBuilder.EnableThreadSafetyChecks();
            //optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            base.OnConfiguring(optionsBuilder);
        }


        #region easy oder

        public DbSet<EasyOrderRequest> EasyOrderRequests { get; set; }
        public DbSet<EasyOrderCartItem> EasyOrderCartItems { get; set; }
        public DbSet<EasyOrderProduct> EasyOrderProducts { get; set; }


        #endregion


        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<ExpenseCategory> Categories => Set<ExpenseCategory>();


        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseType> WarehouseTypes { get; set; }

        public DbSet<WarehouseZone> WarehouseZones { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentCategory> DocumentCategories { get; set; }
        public DbSet<DocumentFolder> DocumentFolders { get; set; }
        public DbSet<DocumentShare> DocumentShares { get; set; }
        public DbSet<DocumentVersion> DocumentVersions { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<UsedAssignedLocation> UsedAssignedLocations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<ProductBarcode> ProductBarcodes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<EmployeeTitle> EmployeeTitles { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Business> Businesses { get; set; }

        public DbSet<BankAccount> Banks { get; set; }

        // marketing automation
        public DbSet<Segment> Segments { get; set; }

        public DbSet<SocialMediaPost> SocialMediaPosts { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<DealFile> DealFiles { get; set; }


        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<MeetingRepeated> MeetingRepeateds { get; set; }
        public DbSet<Note> Notes { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<FollowUpTask> FollowUpTasks { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Automation> Automations { get; set; }
        public DbSet<AutomationStep> AutomationSteps { get; set; }
        public DbSet<AutomationContact> AutomationContacts { get; set; }

        public DbSet<Interaction> Interactions { get; set; }

        // Customer Service
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<ServiceRequestAttachment> ServiceRequestAttachments { get; set; }

        public DbSet<ServiceLevelAgreement> ServiceLevelAgreements { get; set; }
        public DbSet<KnowledgeBaseArticle> KnowledgeBaseArticles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SupportAgent> SupportAgents { get; set; }
        public DbSet<SLAMetrics> SLAMetrics { get; set; }


        // Accounting
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<JournalLine> JournalLines { get; set; }
        public DbSet<GLAccount> GLAccounts { get; set; }

        // AP
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Domain.Entities.AccountsPayable.Invoice> APInvoices { get; set; }
        public DbSet<Payment> Payments { get; set; }

        // AR
        public DbSet<Domain.Entities.AccountsReceivable.Invoice> ARInvoices { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        // Assets
        public DbSet<FixedAsset> FixedAssets { get; set; }
        public DbSet<DepreciationSchedule> DepreciationSchedules { get; set; }

        // Banking
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankTransaction> BankTransactions { get; set; }
        public DbSet<Reconciliation> Reconciliations { get; set; }

        // Budgeting
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Forecast> Forecasts { get; set; }

        public DbSet<LogEntry> Logs { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<SupplierCollaboration> SupplierCollaborations { get; set; }
       

        //    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //    {
        //        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
        //            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        //        {
        //            entry.Entity.DateModified = DateTime.Now;

        //            if (entry.State == EntityState.Added)
        //            {
        //                entry.Entity.DateCreated = DateTime.Now;
        //            }
        //        }

        //        return base.SaveChangesAsync(cancellationToken);
        //    }
    }
}
