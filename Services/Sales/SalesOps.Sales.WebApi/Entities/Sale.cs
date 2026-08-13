using System;

namespace SalesOps.Sales.WebApi.Entities;

public class Sale
{
    public string Id { get; set; } = string.Empty;
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public int StaffId { get; set; }
    public string StaffFirstName { get; set; } = string.Empty;
    public string StaffLastName { get; set; } = string.Empty;
    public string StaffFullName => $"{StaffFirstName} {StaffLastName}";
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public string CategoryId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount => Quantity * UnitPrice;
    public decimal CommissionAmount { get; set; }
    public decimal TotalCommission => Quantity * CommissionAmount;
    public DateTime SaleDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
