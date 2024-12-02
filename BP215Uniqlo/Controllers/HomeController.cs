using BP215Uniqlo.DataAcces;
using BP215Uniqlo.ViewModels.Common;
using BP215Uniqlo.ViewModels.Product;
using BP215Uniqlo.ViewModels.Slider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BP215Uniqlo.Controllers
{
    public class HomeController(UniqloDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            HomeVM vm = new();
            vm.Sliders = await _context.Sliders.Where(x => !x.IsDeleted).Select(x => new SliderItemVM
            {
                ImageUrl = x.ImageURl,
                Link = x.Link,
                SubTitle = x.SubTitle,
                Title = x.Title,
            }).ToListAsync();



            vm.Products = await _context.Product.Where(x => !x.IsDeleted).Select(x => new ProductItemVm
            {
                Discount = x.Discount,
                Id = x.Id,
                ImageUrl = x.CoverImage,
                IsInStock = x.Quantity > 0,
                Name = x.Name,
                Price = x.SellPrice
            }).ToListAsync();
            return View(vm);
        //    vm.Products = await _context.Product
        //        .Where(x => !x.IsDeleted)
        //        .Select(x => new ProductItemVm
        //        {
        //            Discount = x.Discount,
        //            Id = x.Id,
        //            ImageUrl = x.CoverImage,
        //            IsInStock = x.Quantity > 0,
        //            Name = x.Name,
        //            Price = x.SellPrice
        //        }).ToListAsync();
        //    return View(vm);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}