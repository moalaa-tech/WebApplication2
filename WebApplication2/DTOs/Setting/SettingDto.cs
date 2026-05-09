namespace CRM.WebApp.DTOs.Setting
{
    public class SettingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

    }
}
