namespace CRM.WebApp.ViewModels
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public string ProductName { get; set; }

        public int ItemsCount { get; set; }

        public long UTMCampaign { get; set; }
        public string Note { get; set; }
        public string UTMSource { get; set; }

    }
}
