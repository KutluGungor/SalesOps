using MediatR;
using SalesOps.Employee.Application.Features.Staffs.Results;

namespace SalesOps.Employee.Application.Features.Staffs.Queries;

public class GetAllStaffByCompanyIdQuery : IRequest<List<StaffResult>>
{
    public int CompanyId { get; set; }

    public GetAllStaffByCompanyIdQuery(int companyId)
    {
        CompanyId = companyId;
    }
}

