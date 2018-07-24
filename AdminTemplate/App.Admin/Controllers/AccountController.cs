using App.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAdminUserService service;

        public AccountController(IAdminUserService service)
        {
            this.service = service;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult ForgottenPassword() => View();

        [Authorize]
        public IActionResult ManageAccount() => View();

        [HttpPost]
        public IActionResult ForgottenPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "Favor informar o e-mail.");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string pass)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                ModelState.AddModelError("", "Favor informar o e-mail e senha");
                return View();
            }

            var adminUser = service.SignIn(email, pass);
            if (adminUser == null)
            {
                ModelState.AddModelError("", "E-mail ou senha inválidos");
                return View();
            }
            else
            {
                var claims = new Claim[] {
                       new Claim("Id", Convert.ToString(adminUser.Id), ClaimValueTypes.Integer),
                       new Claim("Name", adminUser.Name, ClaimValueTypes.String),
                       new Claim("Email", adminUser.Email, ClaimValueTypes.String)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}