using System;

namespace SalesOps.Sales.WebApi.Dtos;

public class ResultSaleDto
{
    public string Id { get; set; } = string.Empty;
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public int StaffId { get; set; }
    public string StaffFullName { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal CommissionAmount { get; set; }
    public decimal TotalCommission { get; set; }
    public DateTime SaleDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
