using CRM.Domain.Entities.CustomerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.CustomerService
{
    public class ServiceRequestConfiguration : IEntityTypeConfiguration<ServiceRequest>
    {
        public void Configure(EntityTypeBuilder<ServiceRequest> builder)
        {

            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);


            builder.Property(sr => sr.RequestType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sr => sr.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(sr => sr.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

          

            // Relationships
            builder.HasOne(sr => sr.Customer)
                .WithMany(c => c.ServiceRequests)
                .HasForeignKey(sr => sr.CustomerId);


            // Indexes
            builder.HasIndex(sr => sr.Status);
            builder.HasIndex(sr => sr.CustomerId);
            builder.HasIndex(sr => sr.RequestType);
        }
    }
}
