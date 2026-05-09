namespace CRM.WebApp.DTOs.SupplyChainManagement
{
    public class PackageDto
    {
        public string PackageId { get; set; }
        public decimal Weight { get; set; }
        public string WeightUnit { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public string DimensionUnit { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
    }
}
