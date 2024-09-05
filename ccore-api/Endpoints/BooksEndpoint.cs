namespace ccore_api.Endpoints;

public static class BooksEndpoint
{
    public static RouteGroupBuilder MapBooksEndpoint(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("books");

        //V1 Endpoints
        group.MapGet("", GetAllBooks);
        group.MapGet("/{id}", GetBookById);

        return group;
    }

    internal static string GetAllBooks()
    {
        return "Hello from Books";
    }

    internal static int GetBookById(int id)
    {
        return id;
    }
}
