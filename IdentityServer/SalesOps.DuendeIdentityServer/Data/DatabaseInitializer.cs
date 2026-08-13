using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace SalesOps.DuendeIdentityServer.Data;

public static class DatabaseInitializer
{
    public static async Task SeedAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        // Configuration Store seed (Clients, ApiResources, ApiScopes, IdentityResources)
        var configContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        if (!await configContext.Clients.AnyAsync())
        {
            foreach (var client in Config.Clients)
                configContext.Clients.Add(client.ToEntity());
            await configContext.SaveChangesAsync();
            app.Logger.LogInformation("Clients seeded.");
        }

        if (!await configContext.IdentityResources.AnyAsync())
        {
            foreach (var resource in Config.IdentityResources)
                configContext.IdentityResources.Add(resource.ToEntity());
            await configContext.SaveChangesAsync();
            app.Logger.LogInformation("IdentityResources seeded.");
        }

        if (!await configContext.ApiResources.AnyAsync())
        {
            foreach (var resource in Config.ApiResources)
                configContext.ApiResources.Add(resource.ToEntity());
            await configContext.SaveChangesAsync();
            app.Logger.LogInformation("ApiResources seeded.");
        }

        if (!await configContext.ApiScopes.AnyAsync())
        {
            foreach (var scope2 in Config.ApiScopes)
                configContext.ApiScopes.Add(scope2.ToEntity());
            await configContext.SaveChangesAsync();
            app.Logger.LogInformation("ApiScopes seeded.");
        }

        // Admin kullanıcı ve roller seed
        var userManager = scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<Models.ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();

        string[] roles = ["Admin", "Manager", "Staff"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new Microsoft.AspNetCore.Identity.IdentityRole(role));
                app.Logger.LogInformation("Role '{Role}' created.", role);
            }
        }

        if (await userManager.FindByNameAsync("admin") == null)
        {
            var adminUser = new Models.ApplicationUser
            {
                UserName = "admin",
                Email = "admin@salesops.com",
                EmailConfirmed = true,
                CompanyId = 1,
                BranchId = null,
                FirstName = "System",
                LastName = "Admin"
            };

            var result = await userManager.CreateAsync(adminUser, "Admin1234!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                app.Logger.LogInformation("Admin user seeded.");
            }
            else
            {
                app.Logger.LogError("Admin user seed failed: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
