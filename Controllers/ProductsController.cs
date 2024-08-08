using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using ProductApp.Data;
using ProductApp.Models;

namespace ProductApp.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {

            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("AccessDenied", "Home");
            }


            var applicationDbContext = _context.Products.Include(p => p.Category);

            return View(await applicationDbContext.ToListAsync());

        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewBag.kateliste = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductCode,ProductDescription,ProductPicture,ProductPrice,CategoryId,ImageUpload")] Products products)
        {


            if (ModelState.IsValid)
            {
                if (products.ImageUpload != null)
                {
                    var uzanti = Path.GetExtension(products.ImageUpload.FileName);
                    string yeniisim = Guid.NewGuid().ToString() + uzanti;

                    string yol = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Urunler/" + yeniisim);
                    using (var stream = new FileStream(yol, FileMode.Create))
                    {
                        await products.ImageUpload.CopyToAsync(stream);
                    }
                    products.ProductPicture = yeniisim;


                }

                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", products.CategoryId);
            ViewBag.kateliste = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            ViewBag.kateliste = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageUpload,ProductId,ProductName,ProductCode,ProductDescription,ProductPicture,ProductPrice,CategoryId")] Products products)
        {




            if (ModelState.IsValid)
            {
                try
                {
                    if (products.ImageUpload != null)
                    {
                        var uzanti = Path.GetExtension(products.ImageUpload.FileName);
                        string yeniisim = Guid.NewGuid().ToString() + uzanti;

                        string yol = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Urunler/" + yeniisim);
                        using (var stream = new FileStream(yol, FileMode.Create))
                        {
                            await products.ImageUpload.CopyToAsync(stream);
                        }
                        products.ProductPicture = yeniisim;

                    }
                    if (id != products.ProductId)
                    {
                        return NotFound();
                    }
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.kateliste = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(products);
        }




        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }

            string yol = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Urunler/" + products.ProductPicture);
            FileInfo yolFile = new FileInfo(yol);

            if (yolFile.Exists)
            {
                System.IO.File.Delete(yolFile.FullName);
                yolFile.Delete();
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
