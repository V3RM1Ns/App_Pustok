using System.ComponentModel.DataAnnotations;

namespace Pustok.Attributes;

public class FileSizeAttribute:ValidationAttribute
{
    private readonly int _maxFileSize;
    public FileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value is not IFormFile file)
            return ValidationResult.Success;

        if(file.Length > _maxFileSize * 1024 * 1024)
            return new ValidationResult($"File size must be less than {_maxFileSize} MB");

        return ValidationResult.Success;
    }
}