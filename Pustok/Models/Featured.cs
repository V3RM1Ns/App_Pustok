using System.ComponentModel.DataAnnotations;

namespace Pustok.Models;

public class Featured:BaseEntity
{
    [Required]
    public string Title { get; set; }
    public string Icon { get; set; }
    public string Description { get; set; }
}