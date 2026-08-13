using System;

namespace SalesOps.Sales.WebApi.Dtos;

public class CreateSaleDto
{   
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public int StaffId { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime? SaleDate { get; set; } // null ise UtcNow kullan

}
