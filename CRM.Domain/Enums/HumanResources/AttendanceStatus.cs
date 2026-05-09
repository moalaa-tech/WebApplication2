namespace CRM.Domain.Enums.HumanResources
{
    public enum AttendanceStatus
    {
        Present,        // Employee present for full day
        Absent,        // Employee absent without approval
        Late,          // Employee arrived late
        HalfDay,       // Employee worked half day
        OnLeave,       // Employee on approved leave
        Remote,        // Employee working remotely
        BusinessTrip   // Employee on business trip
    }
}
