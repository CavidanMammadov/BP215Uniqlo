namespace BP215Uniqlo.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;
        public IEnumerable<Product>? products{get; set;}
    }
}
