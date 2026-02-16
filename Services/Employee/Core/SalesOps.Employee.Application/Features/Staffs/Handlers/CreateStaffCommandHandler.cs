using MediatR;
using SalesOps.Employee.Application.Features.Staffs.Commands;
using SalesOps.Employee.Application.Interfaces;
using SalesOps.Employee.Domain.Entities;

namespace SalesOps.Employee.Application.Features.Staffs.Handlers;

public class CreateStaffCommandHandler : IRequestHandler<CreateStaffCommand>
{
    private readonly IStaffRepository _staffRepository;

    public CreateStaffCommandHandler(IStaffRepository staffRepository)
    {
        _staffRepository = staffRepository;
    }
    public async Task Handle(CreateStaffCommand request, CancellationToken cancellationToken)
    {
        await _staffRepository.AddAsync(new Staff
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            CompanyId = request.CompanyId,
            BranchId = request.BranchId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
    }
}
