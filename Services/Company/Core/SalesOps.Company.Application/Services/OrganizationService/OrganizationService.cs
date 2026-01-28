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
        return _mapper.Map<ResultOrganizationDto>(organization);
    }

    public async Task<List<ResultOrganizationDto>> GetAllOrganizationsAsync()
    {
        var organizations = await _organizationRepository.GetAllOrganizationsAsync();
        return _mapper.Map<List<ResultOrganizationDto>>(organizations);
    }

    public async Task<ResultOrganizationWithBranchesDto> GetOrganizationWithBranchesAsync(int id)
    {
        var organization = await _organizationRepository.GetOrganizationWithBranchesAsync(id);
        return _mapper.Map<ResultOrganizationWithBranchesDto>(organization);
    }

    public async Task CreateOrganizationAsync(CreateOrganizationDto createOrganizationDto)
    {
        var organization = _mapper.Map<Organization>(createOrganizationDto);
        await _organizationRepository.CreateOrganizationAsync(organization);
    }

    public async Task UpdateOrganizationAsync(UpdateOrganizationDto updateOrganizationDto)
    {
        var organization = await _organizationRepository.GetOrganizationByIdAsync(updateOrganizationDto.Id);
        if (organization != null)
        {
            _mapper.Map(updateOrganizationDto, organization);
            organization.UpdatedAt = DateTime.UtcNow;
            await _organizationRepository.UpdateOrganizationAsync(organization);
        }
        else
        {
            throw new Exception("Organization not found");
        }
    }

    public async Task DeleteOrganizationAsync(int id)
    {
        await _organizationRepository.DeleteOrganizationAsync(id);
    }
}
