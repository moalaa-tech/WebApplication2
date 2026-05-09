namespace CRM.WebApp.DTOs.Banking
{
    public class BankAccountDto
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string Currency { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
