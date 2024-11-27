using BP215Uniqlo.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace BP215Uniqlo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        public async  Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create( ProductCreateVm vm)
        {
            return View();
        }
    }
}
