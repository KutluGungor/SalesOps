using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesOps.Employee.Application.Interfaces;
using SalesOps.Employee.Persistence.Context;
using SalesOps.Employee.Persistence.Repository;

namespace SalesOps.Employee.Persistence.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmployeeContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EmployeeDb")));

            services.AddScoped<IStaffRepository, StaffRepository>();
        }
    }
}
