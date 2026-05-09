using CRM.Domain.Entities.SupplyChainManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.SupplyChainManagement
{
    public class SupplyChainEventDetailConfiguration : IEntityTypeConfiguration<SupplyChainEventDetail>
    {
        public void Configure(EntityTypeBuilder<SupplyChainEventDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);

            builder.Property(x => x.SupplyChainEventId)
                .IsRequired();


            builder.HasOne(x => x.SupplyChainEvent)
                   .WithMany(p => p.Details)
                   .HasForeignKey(x => x.SupplyChainEventId)
                   .OnDelete(DeleteBehavior.Cascade);

           



        }
    }
}
