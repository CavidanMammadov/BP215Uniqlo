﻿namespace BP215Uniqlo.ViewModels.Product
{
    public class ProductIndexVM
    {
        public IEnumerable<ProductItemVm> Products { get; set; }
        public List<CategoryAndCount> Categories { get; set; }

    }
    public class CategoryAndCount
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

    }
}
