using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Enums
{
    public enum ActivityType
    {
        [Display(Name = "Phone Call")]
        Call,
        Email,
        Meeting,
        Task,
        Note,
        [Display(Name = "Follow Up")]
        FollowUp
    }
}
