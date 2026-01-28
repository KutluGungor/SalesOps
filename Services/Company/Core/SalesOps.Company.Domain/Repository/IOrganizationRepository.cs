using SalesOps.Company.Domain.Entity;

namespace SalesOps.Company.Domain.Repository;

public interface IOrganizationRepository
{   
    Task<Organization> GetOrganizationByIdAsync(int id);
    Task<List<Organization>> GetAllOrganizationsAsync();
    Task<Organization> GetOrganizationWithBranchesAsync(int id);
    Task CreateOrganizationAsync(Organization organization);
    Task UpdateOrganizationAsync(Organization organization);
    Task DeleteOrganizationAsync(int id);
}
