using MediatR;

namespace SalesOps.Employee.Application.Features.Staffs.Commands;

public class CreateStaffCommand : IRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
}
