using MediatR;
using SalesOps.Employee.Application.Features.Staffs.Queries;
using SalesOps.Employee.Application.Features.Staffs.Results;
using SalesOps.Employee.Application.Interfaces;

namespace SalesOps.Employee.Application.Features.Staffs.Handlers;

public class GetStaffByIdQueryHandler : IRequestHandler<GetStaffByIdQuery, StaffResult>
{
    private readonly IStaffRepository _staffRepository;

    public GetStaffByIdQueryHandler(IStaffRepository staffRepository)
    {
        _staffRepository = staffRepository;
    }

    public async Task<StaffResult> Handle(GetStaffByIdQuery request, CancellationToken cancellationToken)
    {
        var staff = await _staffRepository.GetStaffByIdAsync(request.Id);
        if (staff == null)
        {
            throw new Exception("Staff not found");
        }
        
        return new StaffResult
        {
            Id = staff.Id,
            FirstName = staff.FirstName,
            LastName = staff.LastName,
            Email = staff.Email,
            Phone = staff.Phone,
            CompanyId = staff.CompanyId,
            BranchId = staff.BranchId,
            CreatedAt = staff.CreatedAt,
            UpdatedAt = staff.UpdatedAt 
        };
    }
}
