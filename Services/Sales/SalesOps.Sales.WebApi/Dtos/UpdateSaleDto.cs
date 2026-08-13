using System;

namespace SalesOps.Sales.WebApi.Dtos;

public class UpdateSaleDto
{    
    public int? CompanyId { get; set; }
    public int? BranchId { get; set; }
    public int? StaffId { get; set; }
    public string? ProductId { get; set; }
    public int? Quantity { get; set; }
    public DateTime? SaleDate { get; set; }
}
