namespace CRM.WebApp.DTOs.Project
{
    public class PhaseExpenseDto
    {
        public int Id { get; set; }
        public int JobPhaseId { get; set; }
        public string PhaseName { get; set; }

        // Expense Details
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }

        // Vendor Information
        public string Vendor { get; set; }
        public string ReceiptNumber { get; set; }
        public string VendorInvoiceNumber { get; set; }

        // Classification
        public string Category { get; set; } // Enum mapped to string
        public bool IsBillable { get; set; }

        // Status
        public string Status { get; set; } // Enum mapped to string
        public bool IsApproved => Status == "Approved" || Status == "Billed";

        // Metadata
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
