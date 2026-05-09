using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.Attributes
{
    // Custom validation attribute for future date
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value is DateTime date && date > DateTime.Now;
        }
    }
}
