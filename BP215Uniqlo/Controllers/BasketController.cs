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
                item = new BasketProductItemVM(id);
                basketItems.Add(item);

            }
            item.Count++;
            Response.Cookies.Append("basket", JsonSerializer.Serialize(basketItems));

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _context.Product.AnyAsync(x => x.Id == id)) return NotFound();
            var basketItems = JsonSerializer.Deserialize<List<BasketProductItemVM>>(Request.Cookies["basket"] ?? "[]");

            var item = basketItems.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                item = new BasketProductItemVM(id);
                basketItems.Add(item);

            }
            item.Count--;
            Response.Cookies.Delete("basket");

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> GetBasket()
        {
            BasketVM vm = new();
            var BasketIds = JsonSerializer.Deserialize<List<BasketProductItemVM>>(Request.Cookies["basket"] ?? "[]");
            var prods = await _context.Product.Where(x => BasketIds.Select(y => y.Id).Any(y => y == x.Id)).Select(x => new ProductBasketItemVM
            {
                Id = x.Id,
                Discount = x.Discount,
                ImageUrl = x.CoverImage,
                Name = x.Name,
                SellPrice = x.SellPrice
            }).ToListAsync();
            foreach (var item in prods)
            {
                item.Count = BasketIds!.FirstOrDefault(x => x.Id == item.Id)!.Count;
            }
            vm.SubTotal = prods.Sum(x => (100 - x.Discount) / 100 * x.SellPrice);
            return PartialView("_BasketPartial" , vm);
        }

        //public async Task<IActionResult> AddProduct(int id)
        //{
        //    if (!await _context.Product.AnyAsync(x => x.Id == id)) return NotFound();
        //    var basketItems = JsonSerializer.Deserialize<List<BasketProductItemVM>>(Request.Cookies["basket"] ?? "[]");

        //    var item = basketItems.FirstOrDefault(x => x.Id == id);
        //    if (item == null)
        //    {
        //        item = new BasketProductItemVM(id );
        //        basketItems.Add(item);

        //    }
        //    item.Count++;
        //    Response.Cookies.Append("basket", JsonSerializer.Serialize(basketItems));
        //    RedirectToAction(nameof(Index));
        //    return RedirectToAction("Index","home");
        //}
    }
}
