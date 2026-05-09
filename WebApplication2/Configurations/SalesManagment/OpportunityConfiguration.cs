using CRM.Domain.Entities.SalesManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.SalesManagment
{
    public class OpportunityConfiguration : IEntityTypeConfiguration<Opportunity>
    {
        public void Configure(EntityTypeBuilder<Opportunity> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).HasDefaultValue(true).IsRequired();


            builder.HasOne(cc => cc.OwnerUser)
                .WithMany(a => a.Opportunities)
                .HasForeignKey(cc => cc.OwnerUserId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Property(bl => bl.EstimatedValue).HasPrecision(18, 2).HasDefaultValue(0);

        }
    }
}
