﻿
namespace Dev.Business.Models
{
    public class Product : Entity
    {
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }
    }
}
