using Microsoft.EntityFrameworkCore;
using SalesOps.Employee.Domain.Entities;

namespace SalesOps.Employee.Persistence.Context;

public class EmployeeContext : DbContext
{
    public EmployeeContext(DbContextOptions<EmployeeContext> options):base(options)
    {
    }
    
    public DbSet<Staff> Staffs { get; set; }
}
