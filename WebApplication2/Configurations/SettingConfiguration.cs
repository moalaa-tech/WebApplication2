using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.NameAr).HasMaxLength(100);

            //builder.HasData(new Setting[] {
            //     new Setting { Id = 1, Name = "Application Title", DateCreated = DateTime.UtcNow },
            //    new Setting { Id = 2, Name = "Default Theme", DateCreated = DateTime.UtcNow },
            //     new Setting { Id = 1, Name = "AppVersion", DateCreated = DateTime.UtcNow },
            //    new Setting { Id = 2, Name = "WelcomeMessage", DateCreated = DateTime.UtcNow }
            //});




        }
    }
}
