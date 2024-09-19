using ccore_api.Authorization;
using ccore_api.Entities;
using ccore_api.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using static ccore_api.Dtos;

namespace ccore_api.Endpoints;

public static class UsersEndpoint
{
    const string GetAuthEndpointName = "GetAuthor";
    public static RouteGroupBuilder MapAuthorEndPoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
        .MapGroup("/authors")
        .WithParameterValidation();

        //V1 Endpoints
        group.MapGet("", GetAllAuthors);

        group.MapGet("/{id}", GetAuthorById)
             .RequireAuthorization(Policies.ReadAccess)
             .WithName(GetAuthEndpointName);

        group.MapPost("", CreateAuthor)
             .RequireAuthorization(Policies.WriteAcess);

        group.MapPut("/{id}", UpdateAuthor)
             .RequireAuthorization(Policies.WriteAcess);

        group.MapDelete("/{id}", DeleteAuthor)
             .RequireAuthorization(Policies.WriteAcess);
        
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

    public static async Task<CreatedAtRoute<AuthorDto>> CreateAuthor(
        IAuthor author,
        CreateAuthor authorDto
    )
    {
        Author auth = new(){
            FirstName = authorDto.FirstName,
            MiddleName = authorDto.MiddleName,
            LastName = authorDto.LastName
        };

        await author.CreateAsync(auth);

        return TypedResults.CreatedAtRoute(auth.AsDto(), GetAuthEndpointName, new {id = auth.Id});
    }

    public static async Task<Results<NotFound, NoContent>> UpdateAuthor(
        IAuthor author,
        UpdateAuthor authorDto,
        int id
    )
    {
        Author? findAuthor = await author.GetAsync(id);

        if(findAuthor is null)
        {
            return TypedResults.NoContent();
        }

        findAuthor.FirstName = authorDto.FirstName;
        findAuthor.MiddleName = authorDto.MiddleName;
        findAuthor.LastName = authorDto.LastName;
        
        await author.UpdateAsync(findAuthor);

        return TypedResults.NoContent();
    }

    public static async Task<NoContent> DeleteAuthor(
        IAuthor author,
        int id
    )
    {
        Author? findAuthor = await author.GetAsync(id);

        if(findAuthor is not null)
        {
            await author.DeleteAsync(id);
        }

        return TypedResults.NoContent();
    }
    
}
