﻿namespace BP215Uniqlo.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int Quantity { get; set; }
        public float  Discount { get; set; }
        public string CoverImage { get; set; } = null!;
        public int?  CategoryId { get; set; }
        public Category? category { get; set; }



    }
}
