using BP215Uniqlo.Models;
using Microsoft.EntityFrameworkCore;

namespace BP215Uniqlo.DataAcces
{
    public class UniqloDbContext:DbContext
    {   
       public  DbSet<Slider> Sliders { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Product> Product {  get; set; } 
        public DbSet<ProductImage> ProductImages {  get; set; } 
        public UniqloDbContext(DbContextOptions opt ) : base(opt) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductImage>( x=> x.Property(y=> y.CreatedTime).HasDefaultValueSql("GETDATE()"));
            base.OnModelCreating(modelBuilder);
        }
    }
}
