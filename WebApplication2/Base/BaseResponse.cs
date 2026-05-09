namespace CRM.WebApp.Base
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
