using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApp.Data;

namespace ProductApp.Component
{
    public class TrendList : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public TrendList(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var result = _context.Products.ToList();
            return View(result);
        }
    }
}
