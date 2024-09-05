using System.ComponentModel.DataAnnotations;

namespace ccore_api.Entities;

public class Author
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public required string FirstName { get; set; }

    [StringLength(150)]
    public required string MiddleName { get; set; }

    [Required]
    [StringLength(150)]
    public required string LastName { get; set; }
    public ICollection<Book> Books { get; } = [];
}
