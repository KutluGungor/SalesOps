using Microsoft.EntityFrameworkCore;
using SalesOps.Company.Domain.Entity;
using SalesOps.Company.Domain.Repository;
using SalesOps.Company.Persistence.Context;

namespace SalesOps.Company.Persistence.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly CompanyContext _context;

    public BranchRepository(CompanyContext context)
    {
        _context = context;
    }

    public async Task<Branch?> GetBranchByIdAsync(int id)
    {
        return await _context.Branches
            .FirstOrDefaultAsync(b => b.Id == id && b.IsActive);
    }

    public async Task CreateBranchAsync(Branch branch)
    {
        branch.CreatedAt = DateTime.UtcNow;
        branch.UpdatedAt = DateTime.UtcNow;

        await _context.Branches.AddAsync(branch);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBranchAsync(Branch branch)
    {
        branch.UpdatedAt = DateTime.UtcNow;
        _context.Branches.Update(branch);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBranchAsync(int id)
    {
        var branch = await _context.Branches
            .FirstOrDefaultAsync(b => b.Id == id && b.IsActive);

        if (branch == null)
            throw new Exception("Branch not found");

        branch.Deactive();
        branch.UpdatedAt = DateTime.UtcNow;

        _context.Branches.Update(branch);
        await _context.SaveChangesAsync();
    }
}
