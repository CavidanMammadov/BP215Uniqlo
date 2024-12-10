using BP215Uniqlo.DataAcces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BP215Uniqlo.Controllers
{
    public class ProductController(UniqloDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Product.Where(x => x.Id == id.Value && !x.IsDeleted)
                .Include(x => x.ProductImages).FirstOrDefaultAsync();
            if (data == null) return NotFound();
            return View(data);
        }
    }
}
