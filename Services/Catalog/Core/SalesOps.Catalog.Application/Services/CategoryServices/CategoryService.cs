using AutoMapper;
using SalesOps.Catalog.Application.Dtos.CategoryDtos;
using SalesOps.Catalog.Domain.Entity;
using SalesOps.Catalog.Domain.Repository;

namespace SalesOps.Catalog.Application.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task AddCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var category = _mapper.Map<Category>(createCategoryDto);
        category.CreatedAt = DateTime.UtcNow;
        category.UpdatedAt = DateTime.UtcNow;
        await _categoryRepository.AddCategoryAsync(category);
    }

    public async Task DeleteCategoryAsync(string companyId, string categoryId)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(companyId, categoryId);
        if (category != null)
        {
            await _categoryRepository.DeleteCategoryAsync(companyId, categoryId);
        }
        else
        {
            throw new Exception("Category not found");
        }
    }

    public async Task<List<ResultCategoryDto>> GetAllCategoriesAsync(string companyId)
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync(companyId);
        return _mapper.Map<List<ResultCategoryDto>>(categories);
    }

    public async Task<ResultCategoryDto> GetCategoryByIdAsync(string companyId, string categoryId)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(companyId, categoryId);
        return _mapper.Map<ResultCategoryDto>(category);
    }

    public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(updateCategoryDto.CompanyId, updateCategoryDto.Id);
        if (category != null)
        {
            _mapper.Map(updateCategoryDto, category);
            category.UpdatedAt = DateTime.UtcNow;
            await _categoryRepository.UpdateCategoryAsync(category);
        }
        else
        {
            throw new Exception("Category not found");
        }
    }

}
