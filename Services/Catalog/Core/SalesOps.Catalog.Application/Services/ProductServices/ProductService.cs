using AutoMapper;
using SalesOps.Catalog.Application.Dtos.CategoryDtos;
using SalesOps.Catalog.Application.Dtos.ProductDtos;
using SalesOps.Catalog.Domain.Entity;
using SalesOps.Catalog.Domain.Repository;

namespace SalesOps.Catalog.Application.Services.ProductServices
{
    public class ProductService : IProductService
    {
        
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        
        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
             _productRepository = productRepository;
             _categoryRepository = categoryRepository;
             _mapper = mapper;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.CreateProductAsync(product);
        }

        public async Task DeleteProductAsync(string companyId, string productId)
        {
            var product = await _productRepository.GetProductByIdAsync(companyId, productId);
            if (product != null)
            {
                await _productRepository.DeleteProductAsync(companyId, productId);
            }else
            {
                throw new Exception("Product not found");
            }   
        }

        public async Task<List<ResultProductDto>> GetAllProductsAsync(string companyId)
        {
            var products =  await _productRepository.GetProductsAllAsync(companyId);
            return _mapper.Map<List<ResultProductDto>>(products);
        }

        public async Task<List<ResultProductDto>> GetAllProductsByCategoryIdAsync(string companyId, string categoryId)
        {
           var products = await _productRepository.GetProductsByCategoryIdAsync(companyId, categoryId);
           return _mapper.Map<List<ResultProductDto>>(products);
        }

        public async Task<ResultProductDto> GetProductByIdAsync(string companyId, string productId)
        {
            var product = await _productRepository.GetProductByIdAsync(companyId, productId);
            return _mapper.Map<ResultProductDto>(product);
        }

        public async Task<ResultProductWithCategoryDto> GetProductWithCategoryAsync(string companyId, string productId)
        {
            var product = await _productRepository.GetProductByIdAsync(companyId, productId);
            if (product == null)
                throw new Exception("Product not found");
                
            var category = await _categoryRepository.GetCategoryByIdAsync(companyId, product.CategoryId);
            
            var result = _mapper.Map<ResultProductWithCategoryDto>(product);
            result.Category = _mapper.Map<ResultCategoryDto>(category);
            
            return result;
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.GetProductByIdAsync(updateProductDto.CompanyId, updateProductDto.Id);
            if (product != null)
            {
                _mapper.Map(updateProductDto, product);
                product.UpdatedAt = DateTime.UtcNow;
                await _productRepository.UpdateProductAsync(product);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
