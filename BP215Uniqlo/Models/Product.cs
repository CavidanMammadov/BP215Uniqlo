using BP215Uniqlo.ViewModels.Product;

namespace BP215Uniqlo.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public string CoverImage { get; set; } = null!;
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Tag> Tags { get; set; }
        
        public IEnumerable<ProductImage>? ProductImages { get; set; }

        public static implicit operator Product(ProductCreateVm vm)
        {
            return new Product
            {

                Name = vm.Name,
                Description = vm.Description,
                CostPrice = vm.CostPrice,
                SellPrice = vm.SellPrice,
                Quantity = vm.Quantity,
                Discount = vm.Discount,
                CategoryId = vm.CategoryId,

            };
        }
        public static implicit operator Product(ProductUpdateVM vm)
        {

            return new Product
            {
                Name = vm.Name,
                Description = vm.Description,
                CostPrice = vm.CostPrice,
                SellPrice = vm.SellPrice,
                Quantity = vm.Quantity,
                Discount = vm.Discount,
                CategoryId = vm.CategoryId,
            };



        }



    }
}
