using Microsoft.EntityFrameworkCore;
using SalesOps.Employee.Application.Interfaces;
using SalesOps.Employee.Domain.Entities;
using SalesOps.Employee.Persistence.Context;

namespace SalesOps.Employee.Persistence.Repository;

public class StaffRepository : IStaffRepository
{
    private readonly EmployeeContext _context;

    public StaffRepository(EmployeeContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Staff staff)
    {
        _context.Staffs.Add(staff);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var staff = await _context.Staffs.FindAsync(id);
        if (staff != null)
        {
            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Staff>> GetAllStaffByBranchIdAsync(int companyId, int branchId)
    {
        return await _context.Staffs
            .Where(s => s.CompanyId == companyId && s.BranchId == branchId)
            .ToListAsync();
    }

    public async Task<List<Staff>> GetAllStaffByCompanyIdAsync(int companyId)
    {
        return await _context.Staffs.Where(s => s.CompanyId == companyId).ToListAsync();
    }

    public async Task<Staff> GetStaffByIdAsync(int id)
    {
        return await _context.Staffs.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task UpdateAsync(Staff staff)
    {
        _context.Staffs.Update(staff);
        await _context.SaveChangesAsync();
    }
}
