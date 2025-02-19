using System;
using System.ComponentModel.DataAnnotations;

namespace FitLogNeo.Validation;

public class PastOrTodayDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateValue && dateValue.ToUniversalTime() > DateTime.UtcNow)
        {
            return new ValidationResult("The date cannot be in the future.");
        }

        return ValidationResult.Success;
    }
}