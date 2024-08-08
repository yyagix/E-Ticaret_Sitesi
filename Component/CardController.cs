using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Data;
using ProductApp.Dto;
using ProductApp.Models;
using ProductApp.Session;
using System.Diagnostics;

namespace ProductApp.Component
{
    [Authorize]
    public class CardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<CardItem> items = HttpContext.Session.GetJson<List<CardItem>>("Card") ?? new List<CardItem>();
            CardViewModel cardViewModel = new()
            {
                CardItems = items
            };
            return View(cardViewModel);
        }
        [Authorize] 
        public async Task<IActionResult> Add(int id)
        {
            Products products = _context.Products.Find(id);
            List<CardItem> items = HttpContext.Session.GetJson<List<CardItem>>("Card") ?? new List<CardItem>();
            CardItem cardItem = items.FirstOrDefault(x => x.ProductId == id);

            if (cardItem == null)
            {
                items.Add(new CardItem(products));
            }
            else
            {
                cardItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Card", items);
            TempData["Mesaj"] = "Ürün Sepete Eklenmiştir";

            return Redirect(Request.Headers["Referer"].ToString());
        }




        public async Task<IActionResult> Decrease(int id)
        {
            List<CardItem> items = HttpContext.Session.GetJson<List<CardItem>>("Card") ?? new List<CardItem>();
            CardItem cardItem = items.FirstOrDefault(c => c.ProductId == id);

            if (cardItem != null)
            {
                if (cardItem.Quantity > 1)
                {
                    cardItem.Quantity -= 1;
                }
                else
                {
                    items.Remove(cardItem);
                }

                HttpContext.Session.SetJson("Card", items);
            }

            TempData["Mesaj"] = "Ürün Miktarı Azaltıldı";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int id)
        {
            List<CardItem> items = HttpContext.Session.GetJson<List<CardItem>>("Card") ?? new List<CardItem>();
            CardItem cardItem = items.FirstOrDefault(c => c.ProductId == id);

            if (cardItem != null)
            {
                items.Remove(cardItem);
                HttpContext.Session.SetJson("Card", items);
            }

            TempData["Mesaj"] = "Ürün Sepetten Silindi";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Clear()
        {
            HttpContext.Session.Remove("Card");
            TempData["Mesaj"] = "Sepet Boşaltıldı";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Increase(int id)
        {
            List<CardItem> items = HttpContext.Session.GetJson<List<CardItem>>("Card") ?? new List<CardItem>();
            CardItem cardItem = items.FirstOrDefault(c => c.ProductId == id);

            if (cardItem != null)
            {
                cardItem.Quantity += 1;
                HttpContext.Session.SetJson("Card", items);
            }

            TempData["Mesaj"] = "Ürün Miktarı Arttırıldı";
            return RedirectToAction("Index");
        }

    }
}
