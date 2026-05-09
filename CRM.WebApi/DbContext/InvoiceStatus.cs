namespace CRM.WebApi.DbContext
{
    public enum InvoiceStatus
    {
        Open = 1,
        PartiallyPaid = 2,
        Paid = 3,
        Overdue = 4,
        Cancelled = 5
    }
}
