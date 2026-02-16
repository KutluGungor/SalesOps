using MediatR;

namespace SalesOps.Employee.Application.Features.Staffs.Commands;

public class CreateStaffCommand : IRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
}
