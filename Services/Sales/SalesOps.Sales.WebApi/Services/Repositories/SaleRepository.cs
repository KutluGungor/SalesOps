using System;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using SalesOps.Sales.WebApi.Entities;
using SalesOps.Sales.WebApi.Services.Interfaces;

namespace SalesOps.Sales.WebApi.Services.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly ElasticsearchClient _elasticsearchClient;
    private const string IndexName = "sales";
    public SaleRepository(ElasticsearchClient elasticsearchClient)
    {
        _elasticsearchClient = elasticsearchClient;
    }
    public async Task<long> CountByCompanyAsync(int companyId, int? branchId = null)
    {
        CountResponse response;
    
        if (branchId.HasValue)
        {
            // CompanyId VE BranchId
            response = await _elasticsearchClient.CountAsync<Sale>(c => c
                .Indices(IndexName)
                .Query(q => q.Bool(b => b
                    .Must(
                        m => m.Term(t => t.Field(f => f.CompanyId).Value(companyId)),
                        m => m.Term(t => t.Field(f => f.BranchId).Value(branchId.Value))
                    )
                ))
            );
        }
        else
        {
            // Sadece CompanyId
            response = await _elasticsearchClient.CountAsync<Sale>(c => c
                .Indices(IndexName)
                .Query(q => q.Term(t => t.Field(f => f.CompanyId).Value(companyId)))
            );
        }
        
        return response.Count;
    }

    public async Task<bool> CreateAsync(Sale sale)
    {
        var response = await _elasticsearchClient.IndexAsync(sale, id => id.Index(IndexName));
        return response.IsValidResponse;

    }

    public async Task<bool> DeleteAsync(string id, int companyId, int? branchId = null)
    {
        // Önce kaydı getir (multi-tenant kontrolü için)
        var sale = await GetByIdAsync(id, companyId, branchId);
        
        if (sale == null)
            return false; // Kayıt yok veya yetki yok
        
        // Silme işlemi
        var response = await _elasticsearchClient.DeleteAsync(IndexName, id);
        
        return response.IsValidResponse;
    }

    public async Task<List<Sale>> GetAllByCompanyAsync(int companyId, int? branchId = null)
    {
        SearchResponse<Sale> response;
        
        if (branchId.HasValue)
        {
            // Branch filtresi varsa: CompanyId VE BranchId
            response = await _elasticsearchClient.SearchAsync<Sale>(s => s.Index(IndexName)
                .Query(q => q.Bool(b => b
                    .Must(
                        m => m.Term(t => t.Field(f => f.CompanyId).Value(companyId)),
                        m => m.Term(t => t.Field(f => f.BranchId).Value(branchId.Value))
                    )
                ))
            );
        }
        else
        {
            // Sadece CompanyId filtresi
            response = await _elasticsearchClient.SearchAsync<Sale>(s => s.Index(IndexName)
                .Query(q => q.Term(t => t.Field(f => f.CompanyId).Value(companyId)))
            );
        }
        
        return response.Documents.ToList();
    }

    public async Task<Sale?> GetByIdAsync(string id, int companyId, int? branchId = null)
    {
        // Elasticsearch'den ID ile kayıt çek + companyId kontrolü
        var response = await _elasticsearchClient.GetAsync<Sale>(id, id => id.Index(IndexName));
        
        if (!response.IsValidResponse || response.Source == null)
            return null;
        
        var sale = response.Source;
        
        // Multi-tenant güvenlik: CompanyId kontrolü
        if (sale.CompanyId != companyId)
            return null;
        
        // Branch filtresi varsa kontrol et
        if (branchId.HasValue && sale.BranchId != branchId.Value)
            return null;
        
        return sale;
    }

    public async Task<List<Sale>> GetByProductAsync(string productId, int companyId, int? branchId = null)
    {
        SearchResponse<Sale> response;
        
        if (branchId.HasValue)
        {
            // 3 filtre: ProductId VE CompanyId VE BranchId
            response = await _elasticsearchClient.SearchAsync<Sale>(s => s.Index(IndexName)
                .Query(q => q.Bool(b => b
                    .Must(
                        m => m.Term(t => t.Field(f => f.ProductId).Value(productId)),
                        m => m.Term(t => t.Field(f => f.CompanyId).Value(companyId)),
                        m => m.Term(t => t.Field(f => f.BranchId).Value(branchId.Value))
                    )
                ))
            );
        }
        else
        {
            // 2 filtre: ProductId VE CompanyId
            response = await _elasticsearchClient.SearchAsync<Sale>(s => s.Index(IndexName)
                .Query(q => q.Bool(b => b
                    .Must(
                        m => m.Term(t => t.Field(f => f.ProductId).Value(productId)),
                        m => m.Term(t => t.Field(f => f.CompanyId).Value(companyId))
                    )
                ))
            );
        }
        
        return response.Documents.ToList();
    }

    public async Task<List<Sale>> GetByStaffAsync(int staffId, int companyId, int? branchId = null)
    {
        SearchResponse<Sale> response;
        
        if (branchId.HasValue)
        {
            // 3 filtre: StaffId VE CompanyId VE BranchId
            response = await _elasticsearchClient.SearchAsync<Sale>(s => s.Index(IndexName)
                .Query(q => q.Bool(b => b
                    .Must(
                        m => m.Term(t => t.Field(f => f.StaffId).Value(staffId)),
                        m => m.Term(t => t.Field(f => f.CompanyId).Value(companyId)),
                        m => m.Term(t => t.Field(f => f.BranchId).Value(branchId.Value))
                    )
                ))
            );
        }
        else
        {
            // 2 filtre: StaffId VE CompanyId
            response = await _elasticsearchClient.SearchAsync<Sale>(s => s.Index(IndexName)
                .Query(q => q.Bool(b => b
                    .Must(
                        m => m.Term(t => t.Field(f => f.StaffId).Value(staffId)),
                        m => m.Term(t => t.Field(f => f.CompanyId).Value(companyId))
                    )
                ))
            );
        }
        
        return response.Documents.ToList();
    }

    public async Task<bool> UpdateAsync(Sale sale)
    {
        var response = await _elasticsearchClient.UpdateAsync<Sale, Sale>(
        IndexName, 
        sale.Id, 
        u => u.Doc(sale));
    
    return response.IsValidResponse;
    }
}
