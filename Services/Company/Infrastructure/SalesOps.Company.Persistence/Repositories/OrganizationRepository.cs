using System;
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
        await _context.Organizations.AddAsync(organization);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrganizationAsync(int id)
    {
        var organization = await _context.Organizations.FindAsync(id);
        if (organization != null)
        {
            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();
        }
    }

      public async Task<List<Organization>> GetAllOrganizationsAsync()
    {
        return await _context.Organizations.ToListAsync();
    }

    public async Task<Organization> GetOrganizationByIdAsync(int id)
    {
        Organization organization = await _context.Organizations.FindAsync(id);
        if (organization == null)
        {
            throw new Exception("Organization not found");
        }
        return organization;
    }

    public async Task<Organization> GetOrganizationWithBranchesAsync(int id)
    {
        var organization = await _context.Organizations
                        .Include(o => o.Branches)
                        .FirstOrDefaultAsync(o => o.Id == id);
        
        if (organization == null)
        {
            throw new Exception("Organization not found");
        }
        
        return organization;
    }

    public async Task UpdateOrganizationAsync(Organization organization)
    {
        _context.Organizations.Update(organization);
        await _context.SaveChangesAsync();
    }
}
