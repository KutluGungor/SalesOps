using System;
using SalesOps.Sales.WebApi.Entities;

namespace SalesOps.Sales.WebApi.Services.Interfaces;

public interface ISaleRepository
{
    /// <summary>Yeni satış kaydı oluşturur</summary>
    Task<bool> CreateAsync(Sale sale);
    
    /// <summary>ID ile satış kaydı getirir (branch filtreli)</summary>
    Task<Sale?> GetByIdAsync(string id, int companyId, int? branchId = null);
    
    /// <summary>Şirkete ait tüm satışları listeler (branch filtreli)</summary>
    Task<List<Sale>> GetAllByCompanyAsync(int companyId, int? branchId = null);
    
    /// <summary>Çalışana ait satışları listeler (branch filtreli)</summary>
    Task<List<Sale>> GetByStaffAsync(int staffId, int companyId, int? branchId = null);
    
    /// <summary>Ürüne ait satışları listeler (branch filtreli)</summary>
    Task<List<Sale>> GetByProductAsync(string productId, int companyId, int? branchId = null);
    
    /// <summary>Mevcut satış kaydını günceller</summary>
    Task<bool> UpdateAsync(Sale sale);
    
    /// <summary>Satış kaydını siler (branch filtreli)</summary>
    Task<bool> DeleteAsync(string id, int companyId, int? branchId = null);
    
    /// <summary>Toplam satış sayısını döner (branch filtreli)</summary>
    Task<long> CountByCompanyAsync(int companyId, int? branchId = null);
}
