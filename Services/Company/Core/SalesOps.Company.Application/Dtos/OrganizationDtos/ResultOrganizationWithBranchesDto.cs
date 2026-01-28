using SalesOps.Company.Application.Dtos.BranchDtos;

namespace SalesOps.Company.Application.Dtos.OrganizationDtos;

public class ResultOrganizationWithBranchesDto
{    
    public int Id { get; set; }
    public string Name { get; set; }
    public string TaxNumber { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public ICollection<ResultBranchDto> Branches { get; set; }
}
