using CRM.WebApp.DTOs.Project;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.Attributes
{
    public class DateAfterProjectStartAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                var instance = (CreateJobPhaseDto)validationContext.ObjectInstance;
                if (dateValue < instance.ProjectStartDate)
                {
                    return new ValidationResult($"Start date cannot be before project start date ({instance.ProjectStartDate:d})");
                }

                if (instance.ProjectEndDate.HasValue && dateValue > instance.ProjectEndDate.Value)
                {
                    return new ValidationResult($"Start date cannot be after project end date ({instance.ProjectEndDate.Value:d})");
                }
            }
            return ValidationResult.Success;
        }
    }
}
