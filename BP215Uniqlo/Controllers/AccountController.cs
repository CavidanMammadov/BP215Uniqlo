using BP215Uniqlo.Models;
using BP215Uniqlo.ViewModels.Auths;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BP215Uniqlo.Controllers
{
    public class AccountController(UserManager<User> _userManager) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View();
            User user = new User
            {
                FullName = vm.FullName,
                Email = vm.Email,
                UserName = vm.UserName,
                    ProfilImageUrl ="photo"
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                } return View();
            }
            return View();
        }
    }
}

