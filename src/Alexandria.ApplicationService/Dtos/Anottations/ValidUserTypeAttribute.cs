using System.ComponentModel.DataAnnotations;

namespace Alexandria.ApplicationService.Dtos.Anottations;

public class ValidUserTypeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var type = value?.ToString().Trim().ToLower();

        if ("default".Equals(type) || "academic".Equals(value))
            return ValidationResult.Success;

        return new ValidationResult(ErrorMessage);
    }
}

