namespace CRM.WebApp.DTOs.Banking
{
    public class CreateBankAccountDto
    {
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string Currency { get; set; }
    }
}
