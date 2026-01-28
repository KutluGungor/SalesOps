using SalesOps.Catalog.Application.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOps.Catalog.Application.Services.ProductServices
{
    public interface IProductService
    {
        Task<ResultProductDto> GetProductByIdAsync(string companyId, string productId);
        Task<List<ResultProductDto>> GetAllProductsAsync(string companyId);
        Task<ResultProductWithCategoryDto> GetProductWithCategoryAsync(string companyId, string productId);
        Task<List<ResultProductDto>> GetAllProductsByCategoryIdAsync(string companyId, string categoryId);
        Task CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        Task DeleteProductAsync(string companyId, string productId);
    }
}
