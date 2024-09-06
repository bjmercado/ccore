using ccore_api.Interfaces;
using ccore_api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ccore_api.Data;

public static class DataExtensions
{
    public static async Task InitializedDBAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connString = configuration.GetConnectionString("AppContext");
        services.AddSqlServer<AppDBContext>(connString)
                .AddScopedServices();
        
        return services;
    }

    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthor, AuthorRepository>();
        services.AddScoped<IBook, BookRepository>();
        return services;
    }
}
