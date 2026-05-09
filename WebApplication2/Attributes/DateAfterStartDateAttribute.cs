using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.Attributes
{
    public class DateAfterStartDateAttribute : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public DateAfterStartDateAttribute(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);
            if (startDateProperty == null)
            {
                return new ValidationResult($"Unknown property: {_startDatePropertyName}");
            }

            var startDateValue = (DateTime?)startDateProperty.GetValue(validationContext.ObjectInstance);

            if (value is DateTime endDateValue && startDateValue.HasValue)
            {
                if (endDateValue < startDateValue.Value)
                {
                    return new ValidationResult("End date must be after start date");
                }
            }
            return ValidationResult.Success;
        }
    }
}
