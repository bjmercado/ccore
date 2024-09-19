using ccore_api.Authorization;
using ccore_api.Entities;
using ccore_api.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using static ccore_api.Dtos;

namespace ccore_api.Endpoints;

public static class BooksEndpoint
{
    const string GetBookEndpointName = "GetBook";
    public static RouteGroupBuilder MapBooksEndpoint(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("books");

        //V1 Endpoints
        group.MapGet("", GetAllBooks);

        group.MapGet("/{id}", GetBookById)
             .RequireAuthorization(Policies.ReadAccess)
             .WithName(GetBookEndpointName);
        
        group.MapPost("", CreateBook)
             .RequireAuthorization(Policies.WriteAcess);

        group.MapPut("/{id}", UpdateBook)
             .RequireAuthorization(Policies.WriteAcess);

        group.MapDelete("/{id}", DeleteBook)
             .RequireAuthorization(Policies.WriteAcess);

        return group;
    }

    public static async Task<Ok<IEnumerable<BookDto>>> GetAllBooks(
        IBook book,
        [AsParameters] GetBooksDto request
    )
    {
        var books = await book.GetAllAsync(
            request.PageNumber,
            request.PageSize,
            request.Filter
        );

        return TypedResults.Ok(books.Select(book => book.AsDto()));
    }

    public static async Task<Results<Ok<BookDto>, NotFound>> GetBookById(
        IBook book,
        int id
    )
    {
        Book? bookIsFound = await book.GetAsync(id);

        return bookIsFound is not null ? TypedResults.Ok(bookIsFound.AsDto()) : TypedResults.NotFound();
    }

    public static async Task<CreatedAtRoute<BookDto>> CreateBook(
        IBook book,
        IAuthor author,
        CreateBook bookDto
    )
    {
        Author? auth = await author.GetAsync(bookDto.AuthorId);
        
        if(auth is null)
        {
            throw new ArgumentOutOfRangeException(nameof(auth), "Author not found");
        }

        Book bk = new(){
            AuthorId = bookDto.AuthorId,
            BookName = bookDto.BookName,
            Genre = bookDto.Genre,
            Price = bookDto.Price,
            ReleaseDate = bookDto.ReleaseDate,
            Author = auth
        };

        await book.CreateAsync(bk);

        return TypedResults.CreatedAtRoute(bk.AsDto(), GetBookEndpointName, new {id = bk.Id});
    }

    public static async Task<Results<NotFound, NoContent>> UpdateBook(
        IBook book,
        IAuthor author,
        UpdateBook bookDto,
        int id
    )
    {
        Author? authorIsFound = await author.GetAsync(bookDto.AuthorId);

        if(authorIsFound is null)
        {
            throw new ArgumentOutOfRangeException(nameof(authorIsFound), "Author not found");
        }

        Book? bookIsFound = await book.GetAsync(id);
        
        if(bookIsFound is null)
        {
            return TypedResults.NotFound();
        }

        bookIsFound.AuthorId = bookDto.AuthorId;
        bookIsFound.BookName = bookDto.BookName;
        bookIsFound.Genre = bookDto.Genre;
        bookIsFound.Price = bookDto.Price;
        bookIsFound.ReleaseDate = bookDto.ReleaseDate;

        await book.UpdateAsync(bookIsFound);

        return TypedResults.NoContent();
    }

    public static async Task<NoContent> DeleteBook(
        IBook book,
        int id
    )
    {
        Book? bookIsFound = await book.GetAsync(id);

        if(bookIsFound is not null)
        {
            await book.DeleteAsync(id);
        }

        return TypedResults.NoContent();
    }
}
