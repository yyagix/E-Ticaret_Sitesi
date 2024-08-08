using Microsoft.AspNetCore.Mvc;
using ProductApp.Data;
using ProductApp.Dto;
using ProductApp.Models;
using ProductApp.Session;

namespace ProductApp.Component
{
    public class CardSumList:ViewComponent 
    {
        private readonly ApplicationDbContext _context;

        public CardSumList(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            List<CardItem> card = HttpContext.Session.GetJson<List<CardItem>>("Card") ?? new List<CardItem>();
            CardViewModel cardViewModel = new()
            {
                CardItems = card
            };
            return View(cardViewModel);
        }

    }
}
