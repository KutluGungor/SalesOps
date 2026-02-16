using MediatR;
using SalesOps.Employee.Application.Features.Staffs.Results;

namespace SalesOps.Employee.Application.Features.Staffs.Queries;

public class GetStaffByIdQuery : IRequest<StaffResult>
{
    public int Id { get; set; }

    public GetStaffByIdQuery(int id)
    {
        Id = id;
    }
}
