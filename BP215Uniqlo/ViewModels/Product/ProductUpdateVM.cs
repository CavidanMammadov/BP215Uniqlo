using BP215Uniqlo.ViewModels.Common;

namespace BP215Uniqlo.ViewModels.Product
{
    public class ProductUpdateVM
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public string CoverFileUrl { get; set; } = null!;
        public IEnumerable<ImageUrlAndId>? OtherImagesUrls { get; set; }
        public IFormFile CoverFile { get; set; } = null!;
        public IEnumerable<IFormFile>? OtherImages { get; set; } 
        public int? CategoryId { get; set; }
   


    }
}
