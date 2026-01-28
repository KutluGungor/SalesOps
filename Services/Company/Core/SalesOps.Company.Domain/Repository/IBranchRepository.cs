using SalesOps.Company.Domain.Entity;

namespace SalesOps.Company.Domain.Repository;

public interface IBranchRepository
{    
    Task<Branch> GetBranchByIdAsync(int id);  // Branch Manager için
    Task CreateBranchAsync(Branch branch);
    Task UpdateBranchAsync(Branch branch);
    Task DeleteBranchAsync(int id);
}
