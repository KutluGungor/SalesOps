using AutoMapper;
using SalesOps.Company.Application.Dtos.OrganizationDtos;
using SalesOps.Company.Domain.Entity;
using SalesOps.Company.Domain.Repository;

namespace SalesOps.Company.Application.Services.OrganizationService;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;

    public OrganizationService(IOrganizationRepository organizationRepository, IMapper mapper)
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
    }

    public async Task<ResultOrganizationDto> GetOrganizationByIdAsync(int id)
    {
        var organization = await _organizationRepository.GetOrganizationByIdAsync(id);

        if (organization == null || !organization.IsActive)
        {
            throw new Exception("Organization not found");
        }

        return _mapper.Map<ResultOrganizationDto>(organization);
    }

    public async Task<List<ResultOrganizationDto>> GetAllOrganizationsAsync()
    {
        var organizations = await _organizationRepository.GetAllOrganizationsAsync();
        organizations = organizations.Where(o => o.IsActive).ToList();
        return _mapper.Map<List<ResultOrganizationDto>>(organizations);
    }

    public async Task<ResultOrganizationWithBranchesDto> GetOrganizationWithBranchesAsync(int id)
    {
        var organization = await _organizationRepository.GetOrganizationWithBranchesAsync(id);

        if (organization == null || !organization.IsActive)
            throw new Exception("Organization not found");

        return _mapper.Map<ResultOrganizationWithBranchesDto>(organization);
    }

    public async Task CreateOrganizationAsync(CreateOrganizationDto dto)
    {
        var organization = _mapper.Map<Organization>(dto);
        organization.CreatedAt = DateTime.UtcNow;
        organization.UpdatedAt = DateTime.UtcNow;
        await _organizationRepository.CreateOrganizationAsync(organization);
    }

    public async Task UpdateOrganizationAsync(UpdateOrganizationDto dto)
    {
        var organization = await _organizationRepository.GetOrganizationByIdAsync(dto.Id);

        if (organization == null)
        {
            throw new Exception("Organization not found");
        }
    
        _mapper.Map(dto, organization);
        organization.UpdatedAt = DateTime.UtcNow;
        await _organizationRepository.UpdateOrganizationAsync(organization);
    }

    public async Task DeleteOrganizationAsync(int id)
    {
        await _organizationRepository.DeleteOrganizationAsync(id);
    }
}
