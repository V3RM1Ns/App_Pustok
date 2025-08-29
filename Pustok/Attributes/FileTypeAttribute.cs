using System.ComponentModel.DataAnnotations;

namespace Pustok.Attributes;

public class FileTypeAttribute:ValidationAttribute
{
    private readonly string[] _allowedTypes;
    public FileTypeAttribute(string[] allowedTypes)
    {
        _allowedTypes = allowedTypes;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value is not IFormFile file)
            return ValidationResult.Success;

        if(!_allowedTypes.Contains(file.ContentType))
            return new ValidationResult($"File type must be one of the following: {string.Join(", ", _allowedTypes)}");

        return ValidationResult.Success;
    }
}