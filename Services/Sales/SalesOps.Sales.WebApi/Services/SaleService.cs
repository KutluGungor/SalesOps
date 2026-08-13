using System.Net.Http.Json;
using SalesOps.Sales.WebApi.Dtos;
using SalesOps.Sales.WebApi.Entities;
using SalesOps.Sales.WebApi.Services.Interfaces;

namespace SalesOps.Sales.WebApi.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _repository;
    private readonly HttpClient _httpClient;
    private const string CatalogProductBaseAddress = "http://localhost:5042";

    public SaleService(ISaleRepository repository, IHttpClientFactory httpClientFactory)
    {
        _repository = repository;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<ResultSaleDto> CreateAsync(CreateSaleDto dto)
    {
        var product = await GetProductFromCatalogAsync(dto.CompanyId, dto.ProductId);
        // TODO: Employee'den personel bilgisi çek
        
        var sale = new Sale
        {
            Id = Guid.NewGuid().ToString(), // Elasticsearch ID
            CompanyId = dto.CompanyId,
            BranchId = dto.BranchId,
            StaffId = dto.StaffId,
            ProductId = dto.ProductId,
            ProductName = product.Name,
            Barcode = product.Barcode,
            CategoryId = product.CategoryId,
            Quantity = dto.Quantity,
            UnitPrice = product.Price,
            CommissionAmount = product.CommissionAmount,
            SaleDate = dto.SaleDate ?? DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
            // ProductName, Barcode, CategoryId, StaffFirstName, StaffLastName, UnitPrice, CommissionAmount -> API'lerden gelecek
        };

        await _repository.CreateAsync(sale);
        return MapToDto(sale);
    }

    public async Task<ResultSaleDto?> GetByIdAsync(string id, int companyId, int? branchId = null)
    {
        var sale = await _repository.GetByIdAsync(id, companyId, branchId);
        return sale == null ? null : MapToDto(sale);
    }

    public async Task<List<ResultSaleDto>> GetAllByCompanyAsync(int companyId, int? branchId = null)
    {
        var sales = await _repository.GetAllByCompanyAsync(companyId, branchId);
        return sales.Select(MapToDto).ToList();
    }

    public async Task<List<ResultSaleDto>> GetByStaffAsync(int staffId, int companyId, int? branchId = null)
    {
        var sales = await _repository.GetByStaffAsync(staffId, companyId, branchId);
        return sales.Select(MapToDto).ToList();
    }

    public async Task<List<ResultSaleDto>> GetByProductAsync(string productId, int companyId, int? branchId = null)
    {
        var sales = await _repository.GetByProductAsync(productId, companyId, branchId);
        return sales.Select(MapToDto).ToList();
    }

    public async Task<ResultSaleDto> UpdateAsync(string id, UpdateSaleDto dto, int companyId, int? branchId = null)
    {
        // Mevcut kaydı getir
        var sale = await _repository.GetByIdAsync(id, companyId, branchId);
        if (sale == null)
            throw new Exception("Satış kaydı bulunamadı veya yetkisiz erişim");

        // Partial update: sadece dolu alanları güncelle
        if (dto.StaffId.HasValue) sale.StaffId = dto.StaffId.Value;
        if (dto.ProductId != null) sale.ProductId = dto.ProductId;
        if (dto.Quantity.HasValue) sale.Quantity = dto.Quantity.Value;
        if (dto.SaleDate.HasValue) sale.SaleDate = dto.SaleDate.Value;
        
        // TODO: StaffId veya ProductId değiştiyse API'lerden yeni bilgileri çek
        
        sale.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(sale);
        return MapToDto(sale);
    }

    public async Task<bool> DeleteAsync(string id, int companyId, int? branchId = null)
    {
        return await _repository.DeleteAsync(id, companyId, branchId);
    }

    public async Task<long> CountByCompanyAsync(int companyId, int? branchId = null)
    {
        return await _repository.CountByCompanyAsync(companyId, branchId);
    }

    private async Task<CatalogProductDto> GetProductFromCatalogAsync(int companyId, string productId)
    {
        var requestUri = $"{CatalogProductBaseAddress}/api/Product/{Uri.EscapeDataString(productId)}?companyId={companyId}";
        using var response = await _httpClient.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Catalog product lookup failed for '{productId}' (status {response.StatusCode}).");
        }

        var product = await response.Content.ReadFromJsonAsync<CatalogProductDto>();

        if (product == null)
        {
            throw new InvalidOperationException($"Catalog did not return a product for '{productId}'.");
        }

        return product;
    }

    private sealed record CatalogProductDto(string Id, string CompanyId, string CategoryId, string Barcode, string Name, decimal Price, decimal CommissionAmount);

    // Sale -> ResultSaleDto mapping
    private ResultSaleDto MapToDto(Sale sale)
    {
        return new ResultSaleDto
        {
            Id = sale.Id,
            CompanyId = sale.CompanyId,
            BranchId = sale.BranchId,
            StaffId = sale.StaffId,
            StaffFullName = sale.StaffFullName, // Computed property (FirstName + LastName)
            ProductId = sale.ProductId,
            ProductName = sale.ProductName,
            Barcode = sale.Barcode,
            Quantity = sale.Quantity,
            UnitPrice = sale.UnitPrice,
            TotalAmount = sale.TotalAmount,
            CommissionAmount = sale.CommissionAmount,
            TotalCommission = sale.TotalCommission,
            SaleDate = sale.SaleDate,
            CreatedAt = sale.CreatedAt,
            UpdatedAt = sale.UpdatedAt
        };
    }
}
