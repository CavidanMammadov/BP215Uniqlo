using Microsoft.AspNetCore.Mvc;

namespace BP215Uniqlo.wwwroot.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
