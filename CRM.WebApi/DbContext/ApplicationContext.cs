using CRM.WebApi.DbContext.EasyOrderModels;
using CRM.WebApi.IdentityEntity;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CRM.WebApi.DbContext
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>, IDataProtectionKeyContext
    {
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public DbSet<LogEntry> Logs {  get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignTypes> CampaignTypes { get; set; }


        public DbSet<EasyOrderRequest> EasyOrderRequests { get; set; }
        public DbSet<EasyOrderCartItem> EasyOrderCartItems { get; set; }
        public DbSet<EasyOrderProduct> EasyOrderProducts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.EnableSensitiveDataLogging();
            //optionsBuilder.EnableThreadSafetyChecks();
            //optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            base.OnConfiguring(optionsBuilder);
        }


        
    }
}
