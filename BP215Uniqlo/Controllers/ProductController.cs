using BP215Uniqlo.DataAcces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
                .Include(x => x.ProductImages).Include( x=> x.Ratings).ThenInclude( x=> x.User).FirstOrDefaultAsync();
            if (data == null) return NotFound();
            ViewBag.Rating = 5;
            if (User.Identity?.IsAuthenticated ?? false)
            {
                string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
                int rating = await _context.ProductRatings.Where(x => x.UserId == userId
                  && x.ProductId == id).Select(x => x.Rating).FirstOrDefaultAsync();
                ViewBag.Rating = rating == 0 ? 5 : rating;
            }
            return View(data);
        }
        public async Task<IActionResult> Rating(int productId, int rating)
        {
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            var data = await _context.ProductRatings.Where(x => x.UserId == userId && x.ProductId == x.ProductId).FirstOrDefaultAsync();
            if (data is null)
            {
                await _context.ProductRatings.AddAsync(new Models.ProductRating
                {
                    UserId = userId,
                    ProductId = productId,
                    Rating = rating

                });
            }
            else
            {
                data.Rating = rating;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = productId});

        }
    }
}
