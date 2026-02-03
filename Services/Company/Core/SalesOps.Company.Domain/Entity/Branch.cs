namespace SalesOps.Company.Domain.Entity;

public class Branch
{    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public int OrganizationId { get; set; } 
    public Organization Organization { get; set; }

    public void Deactive()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

}
