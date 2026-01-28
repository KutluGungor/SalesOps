using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOps.Catalog.Domain.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string CompanyId { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public decimal CommissionAmount { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        [BsonIgnore]
        public Category Category { get; set; }
    }
}
