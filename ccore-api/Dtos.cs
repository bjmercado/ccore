using System.ComponentModel.DataAnnotations;
using ccore_api.Entities;

namespace ccore_api;

public class Dtos
{
    #region AUTHOR
    public record GetAuthorsDto(
        int PageNumber = 1,
        int PageSize = 5,
        string? Filter = null
    );

    public record AuthorDto(
        int Id,
        string FirstName,
        string? MiddleName,
        string LastName,
        ICollection<Book> Books
    );

    public record CreateAuthor(
        [Required] [StringLength(150)] string FirstName,
        [StringLength(150)] string MiddleName,
        [Required] [StringLength(150)] string LastName
    );

    public record UpdateAuthor(
        [Required] [StringLength(150)] string FirstName,
        [StringLength(150)] string MiddleName,
        [Required] [StringLength(150)] string LastName
    );
    #endregion

    #region BOOK
    public record GetBooksDto(
        int PageNumber = 1,
        int PageSize = 5,
        string? Filter = null
    );

    public record BookDto(
        int Id,
        int AuthorId,
        string BookName,
        string Genre,
        decimal Price,
        DateTime ReleaseDate,
        Author Author
    );

    public record CreateBook(
        int AuthorId,
        [Required] [StringLength(150)] string BookName,
        [Required] [StringLength(100)] string Genre,
        [Range(1, 100)] decimal Price,
        DateTime ReleaseDate
    );

    public record UpdateBook(
        int AuthorId,
        [Required] [StringLength(150)] string BookName,
        [Required] [StringLength(100)] string Genre,
        [Range(1, 100)] decimal Price,
        DateTime ReleaseDate
    );
    
    #endregion
}
