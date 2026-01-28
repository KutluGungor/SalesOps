using Microsoft.AspNetCore.Mvc;
using SalesOps.Company.Application.Dtos.OrganizationDtos;
using SalesOps.Company.Application.Services.OrganizationService;

namespace SalesOps.Company.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var organizations = await _organizationService.GetAllOrganizationsAsync();
        return Ok(organizations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var organization = await _organizationService.GetOrganizationByIdAsync(id);
        if (organization == null)
        {
            return NotFound();
        }
        return Ok(organization);
    }
    
    [HttpGet("with-branches/{id}")]
    public async Task<IActionResult> GetOrganizationWithBranches(int id)
    {
        var organization = await _organizationService.GetOrganizationWithBranchesAsync(id);
        if (organization == null)
        {
            return NotFound();
        }
        return Ok(organization);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrganizationDto createOrganizationDto)
    {
        await _organizationService.CreateOrganizationAsync(createOrganizationDto);
        return Ok("Organization created successfully");
    }
    [HttpPut]
    public async Task<IActionResult> Update(UpdateOrganizationDto updateOrganizationDto)
    {
        await _organizationService.UpdateOrganizationAsync(updateOrganizationDto);
        return Ok("Organization updated successfully");    
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _organizationService.DeleteOrganizationAsync(id);
        return Ok("Organization deleted successfully"); 
    }



}