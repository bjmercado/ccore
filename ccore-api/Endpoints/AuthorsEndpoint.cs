using ccore_api.Entities;
using ccore_api.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using static ccore_api.Dtos;

namespace ccore_api.Endpoints;

public static class UsersEndpoint
{
    public static RouteGroupBuilder MapAuthorEndPoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
        .MapGroup("/authors")
        .WithParameterValidation();

        //V1 Endpoints
        group.MapGet("", GetAllAuthors);
        group.MapGet("/{id}", GetAuthorById);

        return group;
    }

    public static async Task<Ok<IEnumerable<AuthorDto>>> GetAllAuthors(
        IAuthor author,
        [AsParameters] GetAuthorsDto request
    )
    {
        var authors = await author.GetAllAsync(
            request.PageNumber,
            request.PageSize,
            request.Filter
        );

        return TypedResults.Ok(authors.Select(author => author.AsDto()));
    }

    public static async Task<Results<Ok<AuthorDto>, NotFound>> GetAuthorById(
        IAuthor author,
        int id
    )
    {
        Author? auth = await author.GetAsync(id);
        return auth is not null ? TypedResults.Ok(auth.AsDto()) : TypedResults.NotFound();
    }
    
}
