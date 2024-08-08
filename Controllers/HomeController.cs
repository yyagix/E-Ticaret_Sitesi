using Microsoft.AspNetCore.Mvc;
using ProductApp.Data;
using ProductApp.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
namespace ProductApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _context.Products.FindAsync(id);
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
