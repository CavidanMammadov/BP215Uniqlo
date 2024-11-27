using BP215Uniqlo.DataAcces;
using BP215Uniqlo.ViewModels.Slider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BP215Uniqlo.Controllers
{
    public class HomeController(UniqloDbContext _context     ) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var datas = await _context.Sliders.Select(x => new SliderItemVM
            {

                ImageUrl = x.ImageURl,
                Link = x.Link,
                SubTitle = x.SubTitle,
                Title = x.Title


            }).ToListAsync();
            return View(datas);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
