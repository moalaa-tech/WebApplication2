namespace CRM.WebApp.ViewModels.CustomerService
{
    public class SupportAgentEditViewModel : SupportAgentCreateViewModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;




    }
}
