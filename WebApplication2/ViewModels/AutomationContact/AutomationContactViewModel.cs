namespace CRM.WebApp.ViewModels.AutomationContact
{
    public class AutomationContactViewModel
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public DateTime DateAdded { get; set; }
        public int CurrentStep { get; set; }
        public string CurrentStepName { get; set; }
        public DateTime? NextStepDate { get; set; }
    }
}
