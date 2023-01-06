using Microsoft.AspNetCore.Mvc;
using Reservation_System_.Models;
using Reservation_System_.Models.ViewModels;
using Reservation_System_.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Text;
using XSystem.Security.Cryptography;

namespace Reservation_System_.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Login()
        {
            return View();
        }
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        public static string ComputeSHA256Hash(string text)
        {
            using (var sha256 = new SHA256Managed())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", "");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            model.PasswordProvider = ComputeSHA256Hash(model.PasswordProvider);
            bool userExists = _context.User.Any(x => x.email.Equals(model.EmailProvider) && x.passwordHashcode.Equals(model.PasswordProvider));
            User user = _context.User.FirstOrDefault(x => x.email.Equals(model.EmailProvider) && x.passwordHashcode.Equals(model.PasswordProvider));
            if (userExists)
            {
                var claims = new List<Claim>
             {
            new Claim(ClaimTypes.Name, user.name),
            new Claim("email", user.email)
            };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme
                    );

                var authProperties = new AuthenticationProperties
                {

                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Nieprawidłowy adres emal bądź hasło!");
                return View();
            }
        }
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            user.passwordHashcode = ComputeSHA256Hash(user.passwordHashcode);
            _context.Add(user);
            _context.SaveChanges();
            return View();
        }
        public async Task<IActionResult> SignOutUser(User user)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
