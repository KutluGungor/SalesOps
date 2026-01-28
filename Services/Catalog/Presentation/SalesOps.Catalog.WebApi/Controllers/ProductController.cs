using Microsoft.AspNetCore.Mvc;
using SalesOps.Catalog.Application.Dtos.ProductDtos;
using SalesOps.Catalog.Application.Services.ProductServices;

namespace SalesOps.Catalog.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string companyId)
    {
        var products = await _productService.GetAllProductsAsync(companyId);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string companyId, string id)
    {
        var product = await _productService.GetProductByIdAsync(companyId, id);
        return Ok(product);
    }

    [HttpGet("with-category/{id}")]
    public async Task<IActionResult> GetWithCategory(string companyId, string id)
    {
        var product = await _productService.GetProductWithCategoryAsync(companyId, id);
        return Ok(product);
    }

    [HttpGet("by-category/{categoryId}")]
    public async Task<IActionResult> GetByCategory(string companyId, string categoryId)
    {
        var products = await _productService.GetAllProductsByCategoryIdAsync(companyId, categoryId);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        await _productService.CreateProductAsync(dto);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateProductDto dto)
    {
        await _productService.UpdateProductAsync(dto);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string companyId, string id)
    {
        await _productService.DeleteProductAsync(companyId, id);
        return Ok();
    }
}
