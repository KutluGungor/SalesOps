using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SalesOps.Catalog.Domain.Repository;
using SalesOps.Catalog.Persistance.Repositories;
using SalesOps.Catalog.Persistance.Settings;

namespace SalesOps.Catalog.Persistance.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // MongoDB Settings
        services.Configure<MongoDbSettings>(
            configuration.GetSection("MongoDbSettings"));
        
        services.AddSingleton<IMongoDbSettings>(sp => 
            sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

        // MongoDB Database
        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var settings = sp.GetRequiredService<IMongoDbSettings>();
            var client = new MongoClient(settings.ConnectionString);
            return client.GetDatabase(settings.DatabaseName);
        });

        // Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}
