using MediatR;
using SalesOps.Employee.Application.Features.Staffs.Results;

namespace SalesOps.Employee.Application.Features.Staffs.Queries;

public class GetAllStaffByBranchIdQuery : IRequest<List<StaffResult>>
{
    public int CompanyId { get; set; }
    public int BranchId { get; set; }

    public GetAllStaffByBranchIdQuery(int companyId, int branchId)
    {
        CompanyId = companyId;
        BranchId = branchId;
    }
}
