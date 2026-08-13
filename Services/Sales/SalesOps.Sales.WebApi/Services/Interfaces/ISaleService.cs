using SalesOps.Sales.WebApi.Dtos;

namespace SalesOps.Sales.WebApi.Services.Interfaces;

public interface ISaleService
{
    // Catalog + Employee'den veri çekip yeni satış oluşturur
    Task<ResultSaleDto> CreateAsync(CreateSaleDto dto);
    
    // ID'ye göre getir (multi-tenant)
    Task<ResultSaleDto?> GetByIdAsync(string id, int companyId, int? branchId = null);
    
    // Şirketteki tüm satışlar
    Task<List<ResultSaleDto>> GetAllByCompanyAsync(int companyId, int? branchId = null);
    
    // Personele göre satışlar
    Task<List<ResultSaleDto>> GetByStaffAsync(int staffId, int companyId, int? branchId = null);
    
    // Ürüne göre satışlar
    Task<List<ResultSaleDto>> GetByProductAsync(string productId, int companyId, int? branchId = null);
    
    // Satış güncelle (partial update)
    Task<ResultSaleDto> UpdateAsync(string id, UpdateSaleDto dto, int companyId, int? branchId = null);
    
    // Satış sil (multi-tenant kontrollü)
    Task<bool> DeleteAsync(string id, int companyId, int? branchId = null);
    
    // Şirketteki satış sayısı
    Task<long> CountByCompanyAsync(int companyId, int? branchId = null);

}
