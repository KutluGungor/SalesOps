using SalesOps.Company.Application.Dtos.OrganizationDtos;

namespace SalesOps.Company.Application.Services.OrganizationService;

public interface IOrganizationService
{    
    Task<ResultOrganizationDto> GetOrganizationByIdAsync(int id);
    Task<List<ResultOrganizationDto>> GetAllOrganizationsAsync();
    Task<ResultOrganizationWithBranchesDto> GetOrganizationWithBranchesAsync(int id);
    Task CreateOrganizationAsync(CreateOrganizationDto organization);
    Task UpdateOrganizationAsync(UpdateOrganizationDto organization);
    Task DeleteOrganizationAsync(int id);
}
