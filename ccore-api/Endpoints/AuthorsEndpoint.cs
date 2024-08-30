namespace ccore_api.Endpoints;

public static class UsersEndpoint
{
    public static RouteGroupBuilder MapAuthorEndPoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/authors");

        //V1 Endpoints
        group.MapGet("", GetAllAuthors);
        group.MapGet("/{id}", GetAuthorById);

        return group;
    }

    internal static string GetAllAuthors()
    {
        return "Hello World!";
    }

    internal static int GetAuthorById(int id)
    {
        return id;
    }
    
}
