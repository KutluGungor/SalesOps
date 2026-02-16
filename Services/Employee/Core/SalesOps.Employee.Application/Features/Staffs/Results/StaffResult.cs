namespace SalesOps.Employee.Application.Features.Staffs.Results;

public class StaffResult
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
