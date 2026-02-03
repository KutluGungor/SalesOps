using Microsoft.EntityFrameworkCore;
using SalesOps.Company.Domain.Entity;
using SalesOps.Company.Domain.Repository;
using SalesOps.Company.Persistence.Context;

namespace SalesOps.Company.Persistence.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly CompanyContext _context;

    public OrganizationRepository(CompanyContext context)
    {
        _context = context;
    }

    public async Task CreateOrganizationAsync(Organization organization)
    {
        // ✅ DÜZELTME: Timestamp'ler SERVICE katmanında set edilmeli, buradan kaldırıldı
        await _context.Organizations.AddAsync(organization);
        await _context.SaveChangesAsync();
    }

    public async Task<Organization?> GetOrganizationByIdAsync(int id)
    {
        // ✅ DÜZELTME: IsActive filtresi kaldırıldı - Service katmanında kontrol edilecek
        return await _context.Organizations.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<List<Organization>> GetAllOrganizationsAsync()
    {
        // ✅ DÜZELTME: IsActive filtresi kaldırıldı - Service katmanı kontrol edecek
        return await _context.Organizations.ToListAsync();
    }

    public async Task<Organization?> GetOrganizationWithBranchesAsync(int id)
    {
        // ✅ DÜZELTME: Organization'da IsActive filtresi kaldırıldı
        // Sadece Branches'de IsActive var (genelde aktif şubeler istenir)
        return await _context.Organizations
            .Include(o => o.Branches.Where(b => b.IsActive))
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task UpdateOrganizationAsync(Organization organization)
    {
      
        _context.Organizations.Update(organization);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrganizationAsync(int id)
    {
        // ✅ DÜZELTME: IsActive filtresi kaldırıldı
        var organization = await _context.Organizations.FirstOrDefaultAsync(o => o.Id == id);
        
        // ✅ DÜZELTME: Exception kaldırıldı - Service katmanında kontrol edilmeli
        if (organization != null)
        {
            // ✅ DÜZELTME: IsActive = false işlemi doğrudan yapıldı (Deactive() metodu yok)
            organization.Deactive();
            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync();
        }
    }
}