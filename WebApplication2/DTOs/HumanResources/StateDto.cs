namespace CRM.WebApp.DTOs.HumanResources
{
    public class StateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
    }
}
