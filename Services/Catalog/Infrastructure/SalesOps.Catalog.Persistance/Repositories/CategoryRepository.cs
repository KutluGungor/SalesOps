using MongoDB.Driver;
using SalesOps.Catalog.Domain.Entity;
using SalesOps.Catalog.Domain.Repository;
using SalesOps.Catalog.Persistance.Settings;

namespace SalesOps.Catalog.Persistance.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMongoCollection<Category> _categoryCollection;

    public CategoryRepository(IMongoDatabase database, IMongoDbSettings settings)
    {
        _categoryCollection = database.GetCollection<Category>(settings.CategoryCollectionName);
    }

    public async Task<Category> GetCategoryByIdAsync(string companyId, string categoryId)
    {
        return await _categoryCollection.Find(c => c.Id == categoryId && c.CompanyId == companyId).FirstOrDefaultAsync();
    }

    public async Task<List<Category>> GetAllCategoriesAsync(string companyId)
    {
        return await _categoryCollection.Find(c => c.CompanyId == companyId).ToListAsync();
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _categoryCollection.InsertOneAsync(category);
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        await _categoryCollection.ReplaceOneAsync(c => c.Id == category.Id && c.CompanyId == category.CompanyId,category);
    }

    public async Task DeleteCategoryAsync(string companyId, string categoryId)
    {
        await _categoryCollection.DeleteOneAsync(c => c.Id == categoryId && c.CompanyId == companyId);
    }
}
