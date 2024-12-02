namespace BP215Uniqlo.Models
{
    public class ProductImage:BaseEntity
    {
        public string FileUrl { get; set; }
        public int? Productid  { get; set; }
        public Product? Product { get; set; }
    }
}
