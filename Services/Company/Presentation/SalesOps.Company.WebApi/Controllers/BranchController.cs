using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesOps.Company.Application.Dtos.BranchDtos;
using SalesOps.Company.Application.Services.BranchService;

namespace SalesOps.Company.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var branch = await _branchService.GetBranchByIdAsync(id);
            return Ok(branch);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBranchDto dto)
        {
            await _branchService.CreateBranchAsync(dto);
            return Ok("Branch created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBranchDto dto)
        {
            await _branchService.UpdateBranchAsync(dto);
            return Ok("Branch updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _branchService.DeleteBranchAsync(id);
            return Ok("Branch deleted successfully");
        }
    }
}
