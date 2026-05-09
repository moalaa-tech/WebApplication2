namespace CRM.WebApp.ViewModels.Automation
{
    public class RunAutomationViewModel
    {
        public int AutomationId { get; set; }
        public string AutomationName { get; set; }

        public List<int> SelectedContactIds { get; set; } = new();
        public List<ContactSelectionViewModel> Contacts { get; set; } = new();

        public class ContactSelectionViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Company { get; set; }
            public bool IsSelected { get; set; }
        }
    }
}
