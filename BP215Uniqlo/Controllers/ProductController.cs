using BP215Uniqlo.DataAcces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using prod = BP215Uniqlo.ViewModels.Product;
using BP215Uniqlo.Models;
using BP215Uniqlo.ViewModels.Product;

namespace BP215Uniqlo.Controllers
{
    public class ProductController(UniqloDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {

            IQueryable<Product> query = _context.Product.Where(x => !x.IsDeleted);
            ProductIndexVM vm = new ProductIndexVM
            {
                Products = await query.Select(x => new prod.ProductItemVm
                {
                    IsInStock = x.Quantity > 0,
                    Discount = x.Discount,
                    Name = x.Name,
                    ImageUrl = x.CoverImage,
                    Price = x.SellPrice,
                    Id = x.Id


                }).ToListAsync(),
                Categories = [new CategoryAndCount { Id = 0, Count = await query.CountAsync(), Name = "All" }]

            };
            var cats = await _context.Categories.Where(x => !x.IsDeleted).Select(x => new CategoryAndCount
            {
                Name = x.Name,
                Id = x.Id,
                Count= x.products.Count()
            }).ToListAsync();
            vm.Categories.AddRange(cats);
            return View(vm);

        }
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Product.Where(x => x.Id == id.Value && !x.IsDeleted)
                .Include(x => x.ProductImages).Include(x => x.Ratings).ThenInclude(x => x.User).FirstOrDefaultAsync();
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
        [HttpGet]
        [HttpPost]
        [Route("Product/Rate")]

        public async Task<IActionResult> Rating(int productId, int rating)
        {
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            var data = await _context.ProductRatings.Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefaultAsync();
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
            return RedirectToAction(nameof(Details), new { id = productId });

        }
        public async Task<IActionResult> Comment(int productId)
        {
            return View();
        }
    }
}
