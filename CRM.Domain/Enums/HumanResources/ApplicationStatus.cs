namespace CRM.Domain.Enums.HumanResources
{
    public enum ApplicationStatus
    {
        Received,       // Application submitted but not reviewed
        UnderReview,    // Application is being evaluated
        Interview,      // Candidate scheduled for interview
        Rejected,       // Application rejected
        Hired           // Candidate hired
    }
}
