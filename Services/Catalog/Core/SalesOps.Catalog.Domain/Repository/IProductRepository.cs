using SalesOps.Catalog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOps.Catalog.Domain.Repository
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(string companyId, string id);
        Task<List<Product>> GetProductsAllAsync(string companyId);
        Task<Product> GetProductWithCategoryAsync(string companyId, string id);
        Task<List<Product>> GetProductsByCategoryIdAsync(string companyId, string categoryId);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(string companyId, string id);
    }
}
