namespace CRM.WebApp.ViewModels.SalesManagement
{
    public class DealviewModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int OpportunityId { get; set; }
        public string OpportunityName { get; set; }
        public decimal FinalValue { get; set; }
        public string Description { get; set; }
        public string DealOwner { get; set; }
        public string Name { get; set; }
        public string AccountName { get; set; }
        public bool Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime ClosingDate { get; set; }
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public List<DealFileViewModel> Files { get; set; } = new List<DealFileViewModel>();

        public DateTime ExpectedCloseDate { get; set; }
    }
}
