using SalesOps.Employee.Domain.Entities;

namespace SalesOps.Employee.Application.Interfaces;

public interface IStaffRepository
{
    Task AddAsync(Staff staff);
    Task UpdateAsync(Staff staff);
    Task DeleteAsync(int id);
    Task<List<Staff>> GetAllStaffByCompanyIdAsync(int companyId);
    Task<List<Staff>> GetAllStaffByBranchIdAsync(int companyId, int branchId);
    Task<Staff> GetStaffByIdAsync(int id);

}
