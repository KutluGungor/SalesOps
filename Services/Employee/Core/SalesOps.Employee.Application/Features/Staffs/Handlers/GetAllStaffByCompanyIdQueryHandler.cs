using System;
using MediatR;
using SalesOps.Employee.Application.Features.Staffs.Queries;
using SalesOps.Employee.Application.Features.Staffs.Results;
using SalesOps.Employee.Application.Interfaces;

namespace SalesOps.Employee.Application.Features.Staffs.Handlers;

public class GetAllStaffByCompanyIdQueryHandler : IRequestHandler<GetAllStaffByCompanyIdQuery, List<StaffResult>>
{
    private readonly IStaffRepository _staffRepository;
    public GetAllStaffByCompanyIdQueryHandler(IStaffRepository staffRepository)
    {
        _staffRepository = staffRepository;
    }
    public async Task<List<StaffResult>> Handle(GetAllStaffByCompanyIdQuery request, CancellationToken cancellationToken)
    {
        var staffs = await _staffRepository.GetAllStaffByCompanyIdAsync(request.CompanyId);
     
        return staffs.Select(staff => new StaffResult
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
        }).ToList();
    }
}
