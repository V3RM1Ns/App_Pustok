using System.ComponentModel.DataAnnotations;

namespace Pustok.Attributes;

public class FileTypeAttribute:ValidationAttribute
{
    private readonly string[] _allowedTypes;
    public FileTypeAttribute(params string[] allowedTypes)
    {
        _allowedTypes = allowedTypes;
    }
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        // Tek dosya kontrolü
        if (value is IFormFile singleFile)
        {
            return ValidateFile(singleFile);
        }

        // Dosya listesi kontrolü
        if (value is IEnumerable<IFormFile> fileList)
        {
            foreach (var file in fileList)
            {
                if (file != null)
                {
                    var result = ValidateFile(file);
                    if (result != ValidationResult.Success)
                        return result;
                }
            }
            return ValidationResult.Success;
        }

        return ValidationResult.Success;
    }

    private ValidationResult? ValidateFile(IFormFile file)
    {
        if (!_allowedTypes.Contains(file.ContentType))
            return new ValidationResult($"File type must be one of the following: {string.Join(", ", _allowedTypes)}");

        return ValidationResult.Success;
    }
}