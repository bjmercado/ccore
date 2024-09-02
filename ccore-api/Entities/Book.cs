using System.ComponentModel.DataAnnotations;

namespace ccore_api.Entities;

public class Book
{
    public int Id { get; set; }

    public int AuthorId { get; set; }

    [Required]
    [StringLength(150)]
    public required string BookName { get; set; }

    [Required]
    [StringLength(100)]
    public required string Genre { get; set; }

    [Range(1, 100)]
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public required Author Author { get; set; }
}
