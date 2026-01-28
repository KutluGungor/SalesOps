using System;
using Microsoft.EntityFrameworkCore;
using SalesOps.Company.Domain.Entity;

namespace SalesOps.Company.Persistence.Context;

public class CompanyContext : DbContext
{

    public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
    {
    }

    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Branch> Branches { get; set; }

}
