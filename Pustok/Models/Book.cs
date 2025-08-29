using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using Pustok.Attributes;

namespace Pustok.Models;

public class Book:AuditEntity
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    
    [FileSize(2)]
    [FileType("image/jpeg","image/png")]
    public string MainImage { get; set; }
    [FileSize(2)]
    [FileType("image/jpeg","image/png")]
    public string HoverImage { get; set; }
    
    [Required]
    public Author Author { get; set; }
    public int AuthorId { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    [Required]
    public decimal Price { get; set; }

    public int? DiscountPrice { get; set; }

    public bool InStock { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsNew { get; set; }
    
    [Required]
    public Genre Genre { get; set; }
    public int GenreId { get; set; }
    
    [FileSize(2)]
    [FileType("image/jpeg","image/png")]
    public List<BookImg> BookImages { get; set; }
    
    [Required]
    public List<BookTag> BookTags { get; set; }
}