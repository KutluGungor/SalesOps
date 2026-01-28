using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOps.Catalog.Domain.Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string CompanyId { get; set; }
    }
}
