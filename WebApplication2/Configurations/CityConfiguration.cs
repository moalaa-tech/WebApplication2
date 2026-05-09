using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.NameAr).HasMaxLength(100);


            builder.HasOne(a => a.State).WithMany(g => g.Cities).HasForeignKey(a => a.StateId)
                .OnDelete(DeleteBehavior.NoAction);

            // Indexes for performance optimization
            builder.HasIndex(a => a.StateId)
                .HasDatabaseName("IX_City_StateId");
                
            builder.HasIndex(a => new { a.IsActive, a.Name })
                .HasDatabaseName("IX_City_Active_Name");
                
            // Index for city name searches
            builder.HasIndex(a => a.Name)
                .HasDatabaseName("IX_City_Name");


        //    builder.HasData(
        //    // Cairo Cities
        //    new City { Id = 1, Name = "New Cairo", NameAr = "القاهرة الجديدة", StateId = 1 },
        //    new City { Id = 2, Name = "Maadi", NameAr = "المعادي", StateId = 1 },
        //    new City { Id = 3, Name = "Heliopolis", NameAr = "مصر الجديدة", StateId = 1 },

        //    // Giza Cities
        //    new City { Id = 4, Name = "6th of October City", NameAr = "مدينة 6 أكتوبر", StateId = 2 },
        //    new City { Id = 5, Name = "Dokki", NameAr = "الدقي", StateId = 2 },
        //    new City { Id = 6, Name = "Sheikh Zayed City", NameAr = "مدينة الشيخ زايد", StateId = 2 }
        //);


        //    builder.HasData(
        //    // Cairo Cities (StateId = 1)
        //    new City
        //    {
        //        Id = 1,
        //        StateId = 1,
        //        Name = "New Cairo / El Tagamoa",
        //        NameAr = "القاهرة الجديدة / التجمع الخامس",
               
        //    },
        //    new City
        //    {
        //        Id = 2,
        //        StateId = 1,
        //        Name = "Heliopolis / Masr El Gedida",
        //        NameAr = "مصر الجديدة",
               
        //    },
        //    new City
        //    {
        //        Id = 3,
        //        StateId = 1,
        //        Name = "Maadi",
        //        NameAr = "المعادي",
                
        //    },
        //    new City
        //    {
        //        Id = 4,
        //        StateId = 1,
        //        Name = "Madinet Nasr",
        //        NameAr = "مدينة نصر",
               
        //    },
        //    new City
        //    {
        //        Id = 5,
        //        StateId = 1,
        //        Name = "Helwan",
        //        NameAr = "حلوان",
                
        //    },

        //    // Giza Cities (StateId = 2)
        //    new City
        //    {
        //        Id = 6,
        //        StateId = 2,
        //        Name = "Sixth of October City",
        //        NameAr = "مدينة 6 أكتوبر",
               
        //    },
        //    new City
        //    {
        //        Id = 7,
        //        StateId = 2,
        //        Name = "Dokki",
        //        NameAr = "الدقي",
               
        //    },
        //    new City
        //    {
        //        Id = 8,
        //        StateId = 2,
        //        Name = "Agouza",
        //        NameAr = "العجوزة",
               
        //    },
        //    new City
        //    {
        //        Id = 9,
        //        StateId = 2,
        //        Name = "Imbaba",
        //        NameAr = "إمبابة",
               
        //    },
        //    new City
        //    {
        //        Id = 10,
        //        StateId = 2,
        //        Name = "Giza (Pyramids Area)",
        //        NameAr = "منطقة الأهرام",
   
        //    }
        //);



        }
    }
}
