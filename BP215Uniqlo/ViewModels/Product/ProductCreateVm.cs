using System.ComponentModel.DataAnnotations;

namespace BP215Uniqlo.ViewModels.Product
{
    public class ProductCreateVm
    {
        [MaxLength(32, ErrorMessage = "Title length must be less than 32"), Required(ErrorMessage = "mehsulun adi qeyd edilmedi")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "description daxil edilmedi")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "Alis qiymeti daxil edilmedi")]
        public decimal CostPrice { get; set; }
        [Required(ErrorMessage = "Satis qiymeti daxil edilmedi")]
        public decimal SellPrice { get; set; }
        [Required(ErrorMessage ="Say daxil edilmedi")]
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public IFormFile CoverImage { get; set; } 
        public int? CategoryId { get; set; }
        

    }
}
