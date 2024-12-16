using BP215Uniqlo.DataAcces;
using BP215Uniqlo.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BP215Uniqlo.ViewComponents
{
    public class HeaderViewComponent(UniqloDbContext _context) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var BasketIds = JsonSerializer.Deserialize<List<BasketProductItemVM>>(Request.Cookies["basket"] ?? "[]");
           var prods =   await _context.Product.Where(x => BasketIds.Select(y => y.Id).Any(y => y == x.Id)).Select(x => new ProductBasketItemVM
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
            return View(prods);
        }

    }
}
