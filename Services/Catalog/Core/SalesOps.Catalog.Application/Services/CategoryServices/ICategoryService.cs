using SalesOps.Catalog.Application.Dtos.CategoryDtos;

namespace SalesOps.Catalog.Application.Services.CategoryServices;

public interface ICategoryService
{
    Task<ResultCategoryDto> GetCategoryByIdAsync(string companyId, string categoryId);
    Task<List<ResultCategoryDto>> GetAllCategoriesAsync(string companyId);
    Task AddCategoryAsync(CreateCategoryDto createCategoryDto);
    Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
    Task DeleteCategoryAsync(string companyId, string categoryId);
}
