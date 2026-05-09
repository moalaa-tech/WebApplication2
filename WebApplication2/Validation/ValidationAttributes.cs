using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CRM.WebApp.Validation
{
    /// <summary>
    /// Validates email format with comprehensive regex
    /// </summary>
    public class StrictEmailAttribute : ValidationAttribute
    {
        private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success; // Allow null, use [Required] for required fields

            var email = value.ToString();
            if (string.IsNullOrWhiteSpace(email))
                return ValidationResult.Success;

            if (!Regex.IsMatch(email, EmailPattern))
                return new ValidationResult("Please enter a valid email address.");

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Validates phone number format (international and local)
    /// </summary>
    public class PhoneNumberAttribute : ValidationAttribute
    {
        private const string PhonePattern = @"^[\+]?[1-9][\d]{0,15}$|^[\(]?[\+]?[0-9]*[\)]?[-\s\.]?[0-9]{2,3}[-\s\.]?[0-9]{3,4}[-\s\.]?[0-9]{3,6}$";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var phone = value.ToString();
            if (string.IsNullOrWhiteSpace(phone))
                return ValidationResult.Success;

            if (!Regex.IsMatch(phone, PhonePattern))
                return new ValidationResult("Please enter a valid phone number.");

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Validates that string contains no HTML/Script injection
    /// </summary>
    public class NoHtmlAttribute : ValidationAttribute
    {
        private static readonly string[] ForbiddenTags = { "<script", "<iframe", "<object", "<embed", "<form", "javascript:", "vbscript:", "onload", "onerror", "onclick" };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var input = value.ToString();
            if (string.IsNullOrWhiteSpace(input))
                return ValidationResult.Success;

            var lowerInput = input.ToLowerInvariant();
            if (ForbiddenTags.Any(tag => lowerInput.Contains(tag)))
                return new ValidationResult("Input contains prohibited content.");

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Validates that decimal is positive
    /// </summary>
    public class PositiveDecimalAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is decimal decimalValue && decimalValue <= 0)
                return new ValidationResult("Value must be greater than zero.");

            if (value is double doubleValue && doubleValue <= 0)
                return new ValidationResult("Value must be greater than zero.");

            if (value is float floatValue && floatValue <= 0)
                return new ValidationResult("Value must be greater than zero.");

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Validates that integer is positive
    /// </summary>
    public class PositiveIntegerAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is int intValue && intValue <= 0)
                return new ValidationResult("Value must be greater than zero.");

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Validates date is in the future
    /// </summary>
    public class FutureDateStrictAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is DateTime dateValue && dateValue <= DateTime.Now)
                return new ValidationResult("Date must be in the future.");

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Validates that string length is within business rules
    /// </summary>
    public class BusinessStringLengthAttribute : StringLengthAttribute
    {
        public BusinessStringLengthAttribute(int maximumLength) : base(maximumLength)
        {
            MinimumLength = 2; // Minimum sensible business name/description length
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be between {MinimumLength} and {MaximumLength} characters long.";
        }
    }
}