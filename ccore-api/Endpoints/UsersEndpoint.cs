namespace ccore_api.Endpoints;

public static class UsersEndpoint
{
    public static RouteGroupBuilder MapUserEndPoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/users");

        //V1 Endpoints
        group.MapGet("", GetAllUsers);
        group.MapGet("/{id}", GetUsersById);

        return group;
    }

    internal static string GetAllUsers()
    {
        return "Hello World!";
    }

    internal static int GetUsersById(int id)
    {
        return id;
    }
    
}
