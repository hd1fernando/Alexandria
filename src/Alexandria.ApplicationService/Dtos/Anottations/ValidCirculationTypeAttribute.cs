using System.ComponentModel.DataAnnotations;

namespace Alexandria.ApplicationService.Dtos.Anottations;

public class ValidCirculationTypeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var type = value?.ToString().Trim().ToLower();

        if ("common".Equals(type) || "restrict".Equals(type))
            return ValidationResult.Success;

        return new ValidationResult(ErrorMessage);
    }
}

