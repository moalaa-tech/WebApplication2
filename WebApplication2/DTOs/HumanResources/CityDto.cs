namespace CRM.WebApp.DTOs.HumanResources
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public int StateId { get; set; }
        public string? StateName { get; set; }
        public StateDto? State { get; set; }
    }
}
