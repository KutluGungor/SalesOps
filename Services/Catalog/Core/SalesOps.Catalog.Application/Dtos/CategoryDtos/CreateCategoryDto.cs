using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOps.Catalog.Application.Dtos.CategoryDtos
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public string CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
