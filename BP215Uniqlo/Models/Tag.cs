namespace BP215Uniqlo.Models
{
    public class Tag :BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Product { get; set; }

    }
}
