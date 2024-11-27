using Microsoft.AspNetCore.Mvc;

namespace BP215Uniqlo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public  IActionResult Create()
        {
            return View();
        }
    }
}
