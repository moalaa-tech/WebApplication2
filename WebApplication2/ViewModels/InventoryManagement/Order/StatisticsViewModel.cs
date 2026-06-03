namespace CRM.WebApp.ViewModels.InventoryManagement.Order
{
    public class StatisticsViewModel
    {
        public int? EmployeeId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public StatisticsData? Statistics { get; set; }
    }

    public class StatisticsData
    {
        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }
        public int ConfirmedOrders { get; set; }
        public int PaidOrders { get; set; }
        public int DeliveredOrders { get; set; }
        public int CanceledOrders { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
