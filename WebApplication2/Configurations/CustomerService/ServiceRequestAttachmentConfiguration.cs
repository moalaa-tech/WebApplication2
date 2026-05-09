using CRM.Domain.Entities.CustomerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.CustomerService
{
    public class ServiceRequestAttachmentConfiguration : IEntityTypeConfiguration<ServiceRequestAttachment>
    {
        public void Configure(EntityTypeBuilder<ServiceRequestAttachment> b)
        {
            b.HasKey(a => a.Id);
            b.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            b.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            b.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);


            b.Property(x => x.FileName).HasMaxLength(255).IsRequired();
            b.HasOne(a => a.ServiceRequest)
             .WithMany(r => r.Attachments)
             .HasForeignKey(a => a.ServiceRequestId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
