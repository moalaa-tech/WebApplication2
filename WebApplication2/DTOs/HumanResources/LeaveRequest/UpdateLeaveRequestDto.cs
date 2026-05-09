namespace CRM.WebApp.DTOs.HumanResources.LeaveRequest
{
    public class UpdateLeaveRequestDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}
