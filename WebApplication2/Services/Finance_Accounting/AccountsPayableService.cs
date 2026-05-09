using AutoMapper;
using CRM.Domain.Entities.AccountsPayable;
using CRM.Domain.Entities.Banking;
using CRM.Domain.Entities.CustomerService;
using CRM.Domain.Enums;
using CRM.WebApp.DTOs.AccountsPayable;
using CRM.WebApp.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public class AccountsPayableService : IAccountsPayableService
    {
        private readonly IRepository<Invoice> InvoiceRepository;
        private readonly IRepository<Vendor> VendorRepository;
        private readonly IRepository<Payment> PaymentRepository;
        private readonly IMapper _mapper;

        public AccountsPayableService(
            IRepository<Invoice> _InvoiceRepository,
            IRepository<Vendor> _VendorRepository,
            IRepository<Payment> _PaymentRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            InvoiceRepository = _InvoiceRepository;
            VendorRepository = _VendorRepository;
            PaymentRepository = _PaymentRepository;
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllAsync()
        {
            var invoices = await InvoiceRepository.GetAll().Include(i => i.Vendor).ToListAsync();
            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }

        public async Task<InvoiceDto> GetByIdAsync(int id)
        {
            var invoice = await InvoiceRepository.GetAll().Include(i => i.Vendor).FirstOrDefaultAsync(i => i.Id == id);
            return _mapper.Map<InvoiceDto>(invoice);
        }

        public async Task CreateAsync(InvoiceDto dto)
        {
            var invoice = _mapper.Map<Invoice>(dto);
            await InvoiceRepository.AddAsync(invoice);
            await InvoiceRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(InvoiceDto dto)
        {
            var invoice = await InvoiceRepository.GetByIdAsync(dto.Id);
            if (invoice == null) return;
            _mapper.Map(dto, invoice);
            await InvoiceRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var invoice = await InvoiceRepository.GetByIdAsync(id);
            if (invoice == null) return;
            InvoiceRepository.Delete(invoice);
            await InvoiceRepository.SaveChangesAsync();
        }

        // Implementation: GetVendorsSelectListAsync
        public async Task<IEnumerable<SelectListItem>> GetVendorsSelectListAsync()
        {
            var vendors = await VendorRepository.GetAll().ToListAsync();
            return vendors.Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Name
            }).ToList();
        }

        // Implementation: RecordPaymentAsync
        public async Task RecordPaymentAsync(Payment payment)
        {
            // Add payment record
            await PaymentRepository.AddAsync(payment);

            // Find related invoice and update PaidAmount and Status
            var invoice = await InvoiceRepository.GetByIdAsync(payment.VendorId);
            if (invoice != null)
            {
                invoice.PaidAmount += payment.Amount;
                if (invoice.PaidAmount >= invoice.Amount)
                    invoice.Status = InvoiceStatus.Paid;
                else if (invoice.DueDate < DateTime.UtcNow)
                    invoice.Status = InvoiceStatus.Overdue;
                else
                    invoice.Status = InvoiceStatus.Open;
            }

            await PaymentRepository.SaveChangesAsync();
            await InvoiceRepository.SaveChangesAsync();
        }

        // Implementation: GenerateInvoiceReportAsync
        public async Task<object> GenerateInvoiceReportAsync(DateTime? startDate, DateTime? endDate)
        {
            var query = InvoiceRepository.GetAll().Include(i => i.Vendor).AsQueryable();

            if (startDate.HasValue)
                query = query.Where(i => i.InvoiceDate >= startDate.Value);
            if (endDate.HasValue)
                query = query.Where(i => i.InvoiceDate <= endDate.Value);

            var invoices = await query.ToListAsync();

            var report = invoices
                .GroupBy(i => i.Vendor.Name)
                .Select(g => new
                {
                    Vendor = g.Key,
                    TotalInvoices = g.Count(),
                    TotalAmount = g.Sum(i => i.Amount),
                    TotalPaid = g.Sum(i => i.PaidAmount),
                    Outstanding = g.Sum(i => i.Amount - i.PaidAmount)
                })
                .ToList();

            return report;
        }
    }
}