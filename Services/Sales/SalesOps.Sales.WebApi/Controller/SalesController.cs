using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesOps.Sales.WebApi.Dtos;
using SalesOps.Sales.WebApi.Services.Interfaces;

namespace SalesOps.Sales.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesController : ControllerBase
    {
    private readonly ISaleService _saleService;

    public SalesController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    // Yeni satış oluştur
    [HttpPost]
    public async Task<ActionResult<ResultSaleDto>> Create(CreateSaleDto dto)
    {
        var result = await _saleService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id, companyId = dto.CompanyId }, result);
    }

    // ID'ye göre getir (multi-tenant)
    [HttpGet("{id}")]
    public async Task<ActionResult<ResultSaleDto>> GetById(string id, [FromQuery] int companyId, [FromQuery] int? branchId = null)
    {
        var result = await _saleService.GetByIdAsync(id, companyId, branchId);
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    // Şirketteki tüm satışlar
    [HttpGet("company")]
    public async Task<ActionResult<List<ResultSaleDto>>> GetByCompany([FromQuery] int companyId, [FromQuery] int? branchId = null)
    {
        var results = await _saleService.GetAllByCompanyAsync(companyId, branchId);
        return Ok(results);
    }

    // Personele göre satışlar
    [HttpGet("staff/{staffId}")]
    public async Task<ActionResult<List<ResultSaleDto>>> GetByStaff(int staffId, [FromQuery] int companyId, [FromQuery] int? branchId = null)
    {
        var results = await _saleService.GetByStaffAsync(staffId, companyId, branchId);
        return Ok(results);
    }

    // Ürüne göre satışlar
    [HttpGet("product/{productId}")]
    public async Task<ActionResult<List<ResultSaleDto>>> GetByProduct(string productId, [FromQuery] int companyId, [FromQuery] int? branchId = null)
    {
        var results = await _saleService.GetByProductAsync(productId, companyId, branchId);
        return Ok(results);
    }

    // Satış güncelle
    [HttpPut("{id}")]
    public async Task<ActionResult<ResultSaleDto>> Update(string id, [FromBody] UpdateSaleDto dto, [FromQuery] int companyId, [FromQuery] int? branchId = null)
    {
        var result = await _saleService.UpdateAsync(id, dto, companyId, branchId);
        return Ok(result);
    }

    // Satış sil
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id, [FromQuery] int companyId, [FromQuery] int? branchId = null)
    {
        var success = await _saleService.DeleteAsync(id, companyId, branchId);
        if (!success)
            return NotFound();
        
        return NoContent();
    }

    // Satış sayısı
    [HttpGet("count")]
    public async Task<ActionResult<long>> Count([FromQuery] int companyId, [FromQuery] int? branchId = null)
    {
        var count = await _saleService.CountByCompanyAsync(companyId, branchId);
        return Ok(count);
    }
    }
}
