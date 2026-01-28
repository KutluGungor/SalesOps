using SalesOps.Catalog.Application.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOps.Catalog.Application.Dtos.ProductDtos
{
    public class ResultProductWithCategoryDto
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal CommissionAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CategoryId { get; set; }
        public ResultCategoryDto Category { get; set; }
    }
}
