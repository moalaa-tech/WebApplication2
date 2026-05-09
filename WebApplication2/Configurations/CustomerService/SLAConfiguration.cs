using CRM.Domain.Entities.CustomerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.CustomerService
{
    public class SLAConfiguration : IEntityTypeConfiguration<ServiceLevelAgreement>
    {
        public void Configure(EntityTypeBuilder<ServiceLevelAgreement> builder)
        {
            builder.ToTable("ServiceLevelAgreements");

            builder.HasKey(sla => sla.Id);

            builder.Property(sla => sla.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(sla => sla.Description)
                .HasMaxLength(1000);

            builder.Property(sla => sla.ResponseTime)
                .IsRequired();

            builder.Property(sla => sla.ResolutionTime)
                .IsRequired();

            builder.Property(sla => sla.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Indexes
            builder.HasIndex(sla => sla.IsActive);
            builder.HasIndex(sla => sla.ServiceType);

            builder.HasOne(sla => sla.Metrics).WithOne(metrics => metrics.SLA)
                        .HasForeignKey<SLAMetrics>(metrics => metrics.Id); // or a different FK



            //builder.HasData(
            //new ServiceLevelAgreement
            //{
            //    Id = 1,
            //    Name = "Standard Support",
            //    Description = "Standard support SLA",
            //    ServiceType = "General",
            //    ResponseTime = 24,
            //    ResolutionTime = 72,
            //    IsActive = true,
            //    EscalationProcess = EscalationProcess.TechnicalSpecialist.ToString()
            //});
        }
    }
}
