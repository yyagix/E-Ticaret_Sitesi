using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Models;
using System.Threading.Tasks;

namespace ProductApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel gelen)
        {
            if (!ModelState.IsValid)
            {
                return View(gelen);
            }

            if (string.IsNullOrEmpty(gelen.UserName) || string.IsNullOrEmpty(gelen.Password))
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı ve şifre boş olamaz.");
                return View(gelen);
            }

            var result = await _signInManager.PasswordSignInAsync(gelen.UserName, gelen.Password, false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(gelen.UserName);

                if (user != null)
                {
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Products");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Giriş başarısız. Lütfen kullanıcı adı ve şifrenizi kontrol edin.");
            return View(gelen);
        }
    }
}
