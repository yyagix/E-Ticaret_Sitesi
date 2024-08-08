using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ProductApp.Models;
using System.Threading.Tasks;

namespace ProductApp.Controllers
{
    public class ConfirmMailController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ConfirmMailController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var value = TempData["Mail"] as string;
            if (string.IsNullOrEmpty(value))
            {
                return RedirectToAction("Index", "Home"); // Eğer e-posta adresi bulunamazsa ana sayfaya yönlendir
            }

            var model = new ConfirmUser { Mail = value };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ConfirmUser gelen)
        {
            if (!ModelState.IsValid)
            {
                return View(gelen);
            }

            if (string.IsNullOrEmpty(gelen.Mail))
            {
                ModelState.AddModelError("", "E-posta adresi bulunamadı.");
                return View(gelen);
            }

            var user = await _userManager.FindByEmailAsync(gelen.Mail);
            if (user == null)
            {
                ModelState.AddModelError("", "E-posta adresi bulunamadı.");
                return View(gelen);
            }

            if (user.ConfirmedCode.ToString() == gelen.ConfirmCode)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "Login");
            }



            ModelState.AddModelError("", "Geçersiz onay kodu. Lütfen tekrar deneyin.");
            return View(gelen);
        }
    }
}
