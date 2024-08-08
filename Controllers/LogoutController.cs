using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Models;

namespace ProductApp.Controllers
{
    public class LogoutController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;

        public LogoutController(Microsoft.AspNetCore.Identity.SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            await _signInManager.SignOutAsync();
            ViewBag.Mesaj = "Sistemden Çıkış Yapıldı!";
            return RedirectToAction("Index", "Home");
        }
    }
}
