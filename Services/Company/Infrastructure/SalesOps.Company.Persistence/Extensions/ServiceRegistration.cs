using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesOps.Company.Domain.Repository;
using SalesOps.Company.Persistence.Context;
using SalesOps.Company.Persistence.Repositories;

namespace SalesOps.Company.Persistence.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.AddDbContext<CompanyContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CompanyDb")));

        
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();

        return services;
    }
}
