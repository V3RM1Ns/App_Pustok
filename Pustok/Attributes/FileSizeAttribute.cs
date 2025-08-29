using System.ComponentModel.DataAnnotations;

namespace Pustok.Attributes;

public class FileSizeAttribute : ValidationAttribute
{
    private readonly int _maxSizeInMB;
    
    public FileSizeAttribute(int maxSizeInMB)
    {
        _maxSizeInMB = maxSizeInMB;
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
        }

        return ValidationResult.Success;
    }

    private ValidationResult? ValidateFile(IFormFile file)
    {
        var maxSizeInBytes = _maxSizeInMB * 1024 * 1024;
        
        if (file.Length > maxSizeInBytes)
            return new ValidationResult($"File size must not exceed {_maxSizeInMB} MB");

        return ValidationResult.Success;
    }
}