using SalesOps.Catalog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOps.Catalog.Domain.Repository
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByIdAsync(string companyId, string categoryId);
        Task<List<Category>> GetAllCategoriesAsync(string companyId);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(string companyId, string categoryId);
    }
}
