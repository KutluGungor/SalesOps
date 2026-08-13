using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using SalesOps.DuendeIdentityServer.Models;

namespace SalesOps.DuendeIdentityServer.Services;

public class CustomProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CustomProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);
        if (user == null) return;

        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new("company_id", user.CompanyId.ToString()),
            new("given_name", user.FirstName ?? ""),
            new("family_name", user.LastName ?? ""),
        };

        if (user.BranchId.HasValue)
            claims.Add(new Claim("branch_id", user.BranchId.Value.ToString()));

        foreach (var role in roles)
            claims.Add(new Claim("role", role));

        context.IssuedClaims.AddRange(claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);
        context.IsActive = user != null;
    }
}
