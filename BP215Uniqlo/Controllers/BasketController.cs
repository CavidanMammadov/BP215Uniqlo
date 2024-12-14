using BP215Uniqlo.DataAcces;
using BP215Uniqlo.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BP215Uniqlo.Controllers
{
    public class BasketController(UniqloDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddProduct(int id)
        {
            if (!await _context.Product.AnyAsync(x => x.Id == id)) return NotFound();
            var basketItems = JsonSerializer.Deserialize<List<BasketProductItemVM>>(Request.Cookies["basket"] ?? "[]");

            var item = basketItems.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                item = new BasketProductItemVM(id );
                basketItems.Add(item);

            }
            item.Count++;
            Response.Cookies.Append("basket", JsonSerializer.Serialize(basketItems));
            RedirectToAction(nameof(Index));
            return RedirectToAction("Index","home");
        }
    }
}
