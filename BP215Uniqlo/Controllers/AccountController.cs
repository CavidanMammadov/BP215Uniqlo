﻿using BP215Uniqlo.Enums;
using BP215Uniqlo.Models;
using BP215Uniqlo.ViewModels.Auths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Net;
using System.Net.Mail;

namespace BP215Uniqlo.Controllers
{
    public class AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager) : Controller
    {
        bool isAuthenticated => User.Identity?.IsAuthenticated ?? false;
        public IActionResult Register()
        {
            if (isAuthenticated) return RedirectToAction("Index", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserCreateVM vm)
        {
            if (isAuthenticated) return RedirectToAction("Index", "Home");
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
            var roleResult = await _userManager.AddToRoleAsync(user, nameof(Roles.User));
            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
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
        public async Task<IActionResult> Login(LoginVM vm, string? ReturnUrl)
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
                    ModelState.AddModelError("", "wait until " + user.LockoutEnd!.Value.ToString("yyyy-MM-dd HH:mm:ss"));


                return View();
            }
            if (string.IsNullOrEmpty(ReturnUrl))
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                    return RedirectToAction("Index", new { Controller = "DashBoard", Area = "Admin" });
            }
            return LocalRedirect(ReturnUrl);
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public async Task<IActionResult> Test()
        {
            SmtpClient smtp = new();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("cavidanbm-bp215@code.edu.az", "bcup uiox satv epjq");
            MailAddress from = new MailAddress("cavidanbm-bp215@code.edu.az","CAVIDAN COMPANY");
            MailAddress to = new("mematisirinov31@gmail.com");
            MailMessage msg = new MailMessage(from, to);
            msg.Subject =  "Security alert!";
            msg.Body = " ee ne dost e dombal memati";
            smtp.Send(msg);
            return Ok("Alindi");
             
        }



    }
}

