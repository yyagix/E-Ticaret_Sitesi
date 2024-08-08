using Microsoft.AspNetCore.Mvc;
using ProductApp.Dto;
using ProductApp.Models;
using ProductApp.Session;

namespace ProductApp.Controllers
{
    public class PaymentsController : Controller
    {
        public IActionResult Index()
        {
            List<CardItem> items = HttpContext.Session.GetJson<List<CardItem>>("Card") ?? new List<CardItem>();
            CardViewModel cardViewModel = new()
            {
                CardItems = items
            };

            if (cardViewModel.GrandTotal <= 0)
            {
                ViewBag.ErrorMessage = "Sepette geçerli ürün bulunmuyor. Lütfen ürün ekleyin.";
                return View("EmptyCart");
            }
            return View(cardViewModel);
        }

        [HttpPost]
        public IActionResult PaymentSuccess(string firstname, string email, string address, string city, string cardname, string cardnumber, string expmonth, string expyear, string cvv)
        {
            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(city) || string.IsNullOrEmpty(cardname) || string.IsNullOrEmpty(cardnumber) ||
                string.IsNullOrEmpty(expmonth) || string.IsNullOrEmpty(expyear) || string.IsNullOrEmpty(cvv))
            {
                TempData["Error"] = "Lütfen tüm alanları doldurun.";
                return RedirectToAction("Index");
            }

            List<CardItem> card = HttpContext.Session.GetJson<List<CardItem>>("Card") ?? new List<CardItem>();
            CardViewModel cardViewModel = new()
            {
                CardItems = card
            };

            HttpContext.Session.Remove("Card");

            ViewBag.FirstName = firstname;

            return View(cardViewModel);
        }
    }
}
