using MediatR;
using SalesOps.Employee.Application.Features.Staffs.Commands;
using SalesOps.Employee.Application.Interfaces;

namespace SalesOps.Employee.Application.Features.Staffs.Handlers;

public class RemoveStaffCommandHandler : IRequestHandler<RemoveStaffCommand>
{
    private readonly IStaffRepository _staffRepository;

    public RemoveStaffCommandHandler(IStaffRepository staffRepository)
    {
        _staffRepository = staffRepository;
    }

    public async Task Handle(RemoveStaffCommand request, CancellationToken cancellationToken)
    {
        var staff = await _staffRepository.GetStaffByIdAsync(request.Id);
        
        if (staff == null)
        { 
            throw new Exception("Staff not found");
        }
            
        await _staffRepository.DeleteAsync(staff.Id);

    }
}
