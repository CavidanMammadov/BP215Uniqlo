using BP215Uniqlo.Models;
using Microsoft.EntityFrameworkCore;

namespace BP215Uniqlo.DataAcces
{
    public class UniqloDbContext:DbContext
    {   
       public  DbSet<Slider> Sliders { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public UniqloDbContext(DbContextOptions opt ) : base(opt) { }
    }
}
