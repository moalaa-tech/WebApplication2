namespace CRM.Domain.Enums.HumanResources
{
    public enum LeaveStatus
    {
        Pending,        // Request submitted but not reviewed
        Approved,       // Leave request approved
        Rejected,       // Leave request denied
        Cancelled       // Leave request cancelled by employee
    }
}
