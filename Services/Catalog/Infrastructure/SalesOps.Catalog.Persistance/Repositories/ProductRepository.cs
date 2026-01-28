using MongoDB.Driver;
using SalesOps.Catalog.Domain.Entity;
using SalesOps.Catalog.Domain.Repository;
using SalesOps.Catalog.Persistance.Settings;

namespace SalesOps.Catalog.Persistance.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _productCollection;

    public ProductRepository(IMongoDatabase database, IMongoDbSettings settings)
    {
        _productCollection = database.GetCollection<Product>(settings.ProductCollectionName);
    }

    public async Task<Product> GetProductByIdAsync(string companyId, string productId)
    {
        return await _productCollection.Find(p => p.Id == productId && p.CompanyId == companyId).FirstOrDefaultAsync();
    }

    public async Task<List<Product>> GetProductsAllAsync(string companyId)
    {
        return await _productCollection.Find(p => p.CompanyId == companyId).ToListAsync();
    }

    public async Task<Product> GetProductWithCategoryAsync(string companyId, string id)
    {
        return await _productCollection.Find(p => p.Id == id && p.CompanyId == companyId).FirstOrDefaultAsync();
    }

    public async Task<List<Product>> GetProductsByCategoryIdAsync(string companyId, string categoryId)
    {
        return await _productCollection.Find(p => p.CompanyId == companyId && p.CategoryId == categoryId).ToListAsync();
    }

    public async Task CreateProductAsync(Product product)
    {
        await _productCollection.InsertOneAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productCollection.ReplaceOneAsync(p => p.Id == product.Id && p.CompanyId == product.CompanyId, product);
    }

    public async Task DeleteProductAsync(string companyId, string productId)
    {
        await _productCollection.DeleteOneAsync(p => p.Id == productId && p.CompanyId == companyId);
    }
}
