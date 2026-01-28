using SalesOps.Company.Application.Dtos.BranchDtos;

namespace SalesOps.Company.Application.Services.BranchService;

public interface IBranchService
{    
    Task<ResultBranchDto> GetBranchByIdAsync(int id);
    Task CreateBranchAsync(CreateBranchDto createBranchDto);
    Task UpdateBranchAsync(UpdateBranchDto updateBranchDto);
    Task DeleteBranchAsync(int id);
}
