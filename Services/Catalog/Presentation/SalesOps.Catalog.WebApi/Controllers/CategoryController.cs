using Microsoft.AspNetCore.Mvc;
using SalesOps.Catalog.Application.Dtos.CategoryDtos;
using SalesOps.Catalog.Application.Services.CategoryServices;

namespace SalesOps.Catalog.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string companyId)
    {
        var categories = await _categoryService.GetAllCategoriesAsync(companyId);
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string companyId, string id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(companyId, id);
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        await _categoryService.AddCategoryAsync(dto);
        return Ok("Category created successfully");
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCategoryDto dto)
    {
        await _categoryService.UpdateCategoryAsync(dto);
        return Ok("Category updated successfully");
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string companyId, string id)
    {
        await _categoryService.DeleteCategoryAsync(companyId, id);
        return Ok("Category deleted successfully");
    }
}
