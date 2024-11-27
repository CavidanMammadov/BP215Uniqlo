using System.ComponentModel.DataAnnotations;

namespace BP215Uniqlo.ViewModels.Product
{
    public class ProductItemVm
    {


        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal CostPrice { get; set; }

        public decimal SellPrice { get; set; }

        public int Quantity { get; set; }
        public float Discount { get; set; }
        public IFormFile CoverImage { get; set; }
        public int? CategoryId { get; set; }

    }
}
