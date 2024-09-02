using static ccore_api.Dtos;

namespace ccore_api.Entities;

public static class EntityExtensions
{
    public static AuthorDto AsDto(this Author author)
    {
        return new AuthorDto(
            author.Id,
            author.FirstName,
            author.MiddleName,
            author.LastName,
            author.Books
        );
    }
}
