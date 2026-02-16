using MediatR;

namespace SalesOps.Employee.Application.Features.Staffs.Commands;

public class RemoveStaffCommand : IRequest
{
    public int Id { get; set; }

    public RemoveStaffCommand(int id)
    {
        Id = id;
    }
}
