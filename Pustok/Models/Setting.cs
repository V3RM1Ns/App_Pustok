using System.ComponentModel.DataAnnotations;

namespace Pustok.Models;

public class Setting : BaseEntity
{
    [Required(ErrorMessage = "Setting key is required.")]
    [StringLength(100, ErrorMessage = "Setting key cannot exceed 100 characters.")]
    public string Key { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Setting value is required.")]
    public string Value { get; set; } = string.Empty;
    
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
