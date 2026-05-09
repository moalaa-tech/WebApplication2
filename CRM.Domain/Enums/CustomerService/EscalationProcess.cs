using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Enums.CustomerService
{
    public enum EscalationProcess
    {
        [Display(Name = "Standard Escalation",
                 Description = "Default escalation path through support tiers",
                 ShortName = "STD",
                 Order = 1)]
        Standard = 0,

        [Display(Name = "Critical Priority",
                 Description = "Immediate escalation for critical business impact",
                 ShortName = "CRIT",
                 Order = 0)]
        Critical = 1,

        [Display(Name = "Technical Specialist",
                 Description = "Direct escalation to technical experts",
                 ShortName = "TECH",
                 Order = 2)]
        TechnicalSpecialist = 2,

        [Display(Name = "Management Review",
                 Description = "Escalate to management for approval",
                 ShortName = "MGR",
                 Order = 3)]
        ManagementReview = 3,

        [Display(Name = "Vendor Escalation",
                 Description = "Engage third-party vendor support",
                 ShortName = "VEND",
                 Order = 4)]
        Vendor = 4,

        [Display(Name = "Executive Escalation",
                 Description = "Executive-level escalation path",
                 ShortName = "EXEC",
                 Order = 5)]
        Executive = 5,

        [Display(Name = "Custom Workflow",
                 Description = "Organization-specific escalation process",
                 ShortName = "CUST",
                 Order = 6)]
        Custom = 6,

        [Display(Name = "No Escalation",
                 Description = "Issues remain with assigned team",
                 ShortName = "NONE",
                 Order = 7)]
        None = 7
    }
}
