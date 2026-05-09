//using CRM.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace CRM.WebApp.Configurations
//{
//    public class AccountConfiguration : IEntityTypeConfiguration<Account>
//    {
//        public void Configure(EntityTypeBuilder<Account> builder)
//        {
//            builder.HasKey(a => a.Id);
//            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
//            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
//            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);
//            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
//            builder.Property(a => a.NameAr).HasMaxLength(200);
//        }
//    }
//}
