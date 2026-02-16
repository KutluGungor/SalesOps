using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesOps.Employee.Application.Features.Staffs.Commands;
using SalesOps.Employee.Application.Features.Staffs.Queries;
using SalesOps.Employee.Application.Features.Staffs.Results;

namespace SalesOps.Employee.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff(CreateStaffCommand command)
        {
            await _mediator.Send(command);
            return Ok("Staff successfully added.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStaff(UpdateStaffCommand command)
        {
            await _mediator.Send(command);
            return Ok("Staff successfully updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            await _mediator.Send(new RemoveStaffCommand(id));
            return Ok("Staff successfully removed.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            var staff = await _mediator.Send(new GetStaffByIdQuery(id));
            return Ok(staff);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetStaffsByBranchId(int companyId, int branchId)
        {
            var staffs = await _mediator.Send(new GetAllStaffByBranchIdQuery(companyId, branchId));
            return Ok(staffs);
        }

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetStaffsByCompanyId(int companyId)
        {
            var staffs = await _mediator.Send(new GetAllStaffByCompanyIdQuery(companyId));
            return Ok(staffs);
        }
    }
}
