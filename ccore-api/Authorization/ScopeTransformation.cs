using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace ccore_api.Authorization;

public class ScopeTransformation : IClaimsTransformation
{
    private const string scopeClaimsName = "scope";
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var scopeClaim = principal.FindFirst(scopeClaimsName);

        if (scopeClaim is null)
        {
            return Task.FromResult(principal);
        }

        var scopes = scopeClaim.Value.Split(' ');

        var originalIdentity = principal.Identity as ClaimsIdentity;
        var identity = new ClaimsIdentity(originalIdentity);

        var originalScopeClaim = identity.Claims.FirstOrDefault(claim => claim.Type == scopeClaimsName);

        if(originalScopeClaim is not null)
        {
            identity.RemoveClaim(originalScopeClaim);
        }

        identity.AddClaims(scopes.Select(scope => new Claim(scopeClaimsName, scope)));

        return Task.FromResult(new ClaimsPrincipal(identity));
    }
}
