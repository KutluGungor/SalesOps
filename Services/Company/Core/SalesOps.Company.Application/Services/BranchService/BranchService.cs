using AutoMapper;
using SalesOps.Company.Application.Dtos.BranchDtos;
using SalesOps.Company.Domain.Entity;
using SalesOps.Company.Domain.Repository;

namespace SalesOps.Company.Application.Services.BranchService;

public class BranchService : IBranchService
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    public BranchService(
        IBranchRepository branchRepository,
        IMapper mapper)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
    }

    public async Task<ResultBranchDto> GetBranchByIdAsync(int id)
    {
        var branch = await _branchRepository.GetBranchByIdAsync(id);

        if (branch == null)
            throw new Exception("Branch not found");

        return _mapper.Map<ResultBranchDto>(branch);
    }

    public async Task CreateBranchAsync(CreateBranchDto dto)
    {
        var branch = _mapper.Map<Branch>(dto);
        await _branchRepository.CreateBranchAsync(branch);
    }

    public async Task UpdateBranchAsync(UpdateBranchDto dto)
    {
        var branch = await _branchRepository.GetBranchByIdAsync(dto.Id);

        if (branch == null)
            throw new Exception("Branch not found");

        _mapper.Map(dto, branch);
        await _branchRepository.UpdateBranchAsync(branch);
    }

    public async Task DeleteBranchAsync(int id)
    {
        await _branchRepository.DeleteBranchAsync(id);
    }
}
