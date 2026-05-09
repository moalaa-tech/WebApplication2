namespace CRM.Domain.Enums.HumanResources
{
    public enum PayrollStatus
    {
        Draft,         // Payroll being prepared
        Calculated,   // Payroll calculated but not approved
        Approved,      // Payroll approved for payment
        Paid,          // Salary paid to employee
        Cancelled      // Payroll cancelled
    }
}
