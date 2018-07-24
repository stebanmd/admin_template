using App.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            if (string.IsNullOrEmpty(email))
                ViewData["send"] = false;
            else
                ViewData["send"] = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string pass)
        {
            if (string.IsNullOrEmpty(email))
                return RedirectToAction(nameof(Login));

            var adminUser = service.GetByEmail(email);

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, adminUser.Name),
                new Claim(ClaimTypes.Email, email)
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}