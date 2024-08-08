using Microsoft.AspNetCore.Mvc;

namespace ProductApp.Component
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
