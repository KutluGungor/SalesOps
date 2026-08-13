using Microsoft.AspNetCore.Identity;

namespace SalesOps.DuendeIdentityServer.Models;

public class ApplicationUser : IdentityUser
{
    public int CompanyId { get; set; }
    public int? BranchId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
