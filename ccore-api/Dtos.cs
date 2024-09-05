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
    
    #endregion
}
