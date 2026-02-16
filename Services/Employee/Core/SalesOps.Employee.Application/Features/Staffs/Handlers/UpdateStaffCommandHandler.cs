using MediatR;
using SalesOps.Employee.Application.Features.Staffs.Commands;
using SalesOps.Employee.Application.Interfaces;

namespace SalesOps.Employee.Application.Features.Staffs.Handlers;

public class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand>
{
    private readonly IStaffRepository _staffRepository;

    public UpdateStaffCommandHandler(IStaffRepository staffRepository)
    {
        _staffRepository = staffRepository;
    }

    public async Task Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
    {
        var staff = await _staffRepository.GetStaffByIdAsync(request.Id);
        if (staff == null)
        {        
            throw new Exception("Staff not found");

        }

         staff.FirstName = request.FirstName;
         staff.LastName = request.LastName;
         staff.Email = request.Email;
         staff.Phone = request.Phone;
         staff.CompanyId = request.CompanyId;
         staff.BranchId = request.BranchId;
         staff.UpdatedAt = DateTime.UtcNow;

        await _staffRepository.UpdateAsync(staff);
        
    }
}
