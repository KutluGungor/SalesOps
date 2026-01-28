namespace SalesOps.Company.Application.Dtos.BranchDtos;

public class CreateBranchDto
{    
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public int OrganizationId { get; set; } 
}
