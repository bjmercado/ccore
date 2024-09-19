using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ccore_api.Authorization;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
    {
        services.AddScoped<IClaimsTransformation, ScopeTransformation>()
        .AddAuthorization(options =>
        {
            options.AddPolicy(Policies.ReadAccess, builder =>
            builder.RequireClaim("scope", "app:read")
                   .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));
            
            options.AddPolicy(Policies.WriteAcess, builder =>
            builder.RequireClaim("scope", "app:write")
                   .RequireRole("Admin")
                   .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));
        });

        return services;
    }
}
