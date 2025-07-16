using Microsoft.AspNetCore.Mvc;
using WebHarcamaTakip.Models;
using WebHarcamaTakip.Services;

namespace WebHarcamaTakip.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                ViewBag.Error = "Kullanıcı adı ve şifre gereklidir!";
                return View(user);
            }

            if (UserService.GetByUsername(user.Username) != null)
            {
                ViewBag.Error = "Bu kullanıcı adı zaten alınmış!";
                return View(user);
            }

            UserService.Add(user);
            // Kayıt olduktan sonra login sayfasına yönlendir
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = UserService.GetByUsername(username);
            if (user != null && user.Password == password)
            {
                // Basit cookie ile oturum başlatıyoruz
                HttpContext.Response.Cookies.Append("username", user.Username);
                return RedirectToAction("Index", "Expenses");
            }
            ViewBag.Error = "Hatalı kullanıcı adı veya şifre!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("username");
            return RedirectToAction("Login");
        }
    }
}
