using BP215Uniqlo.Models;
using BP215Uniqlo.ViewModels.Auths;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BP215Uniqlo.Controllers
{
    public class AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager) : Controller
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
                ProfilImageUrl = "photo"
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
                return View();
            }
            return View();
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid) return View();
            User? user = null;
            if (vm.UserNameOrEmail.Contains('@'))
                user = await _userManager.FindByEmailAsync(vm.UserNameOrEmail);
            else
                user = await _userManager.FindByNameAsync(vm.UserNameOrEmail);
            if (user == null)
            {
                ModelState.AddModelError(" ", "USERNAME OR PASSWORD IS INCORRET");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);
            if (!result.Succeeded)
            {
                if (result.IsNotAllowed)
                
                    ModelState.AddModelError("", "UserName or Password is incorrect");
                if (result.IsLockedOut)
                    ModelState.AddModelError("", "wait until "+ user.LockoutEnd!.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                

            return View();
            }
            return RedirectToAction("Index", "Home");
        }



    }
}

