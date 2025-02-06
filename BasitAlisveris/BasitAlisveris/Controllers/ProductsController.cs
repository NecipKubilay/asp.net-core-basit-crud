using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BasitAlisveris.Data;
using BasitAlisveris.Models;

namespace BasitAlisveris.Controllers
{
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
            return View(await _context.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Product_Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        public JsonResult CategoryList(int id)
        {
            var sonuc = _context.SubCategories
                         .Where(x => x.Category_Id == id)
                         .Select(x => new {
                             x.SubCategory_Id,
                             x.SubCategory_Name
                         })
                         .ToList();

            return Json(sonuc);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["Category_Id"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            ViewData["SubCategory_Id"] = new SelectList(_context.SubCategories, "SubCategory_Id", "SubCategory_Name");

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Product_Id,ProductName,ProductDescription,ProductImage,ProductPrice,Category_Id,SubCategory_Id,Product_Feature")] Product product, IFormFile PictureImage)
        {

            if (PictureImage!=null)
            {
                var uzanti = Path.GetExtension(PictureImage.FileName);

                string yeniisim = Guid.NewGuid().ToString() + uzanti;
                
                string yol = Path.Combine(Directory.GetCurrentDirectory()+"/wwwroot/ProductImages/"+yeniisim);
                using (var stream = new FileStream(yol, FileMode.Create))
                {
                    PictureImage.CopyToAsync(stream);
                }
                product.ProductImage = yeniisim;
            }

            //------------------------------------------------------------------------

            if (!ModelState.IsValid)
            {
                Console.WriteLine("⚠️ ModelState geçerli değil!");
                foreach (var modelError in ModelState.Values)
                {
                    foreach (var error in modelError.Errors)
                    {
                        Console.WriteLine("Validation Hatası: " + error.ErrorMessage);
                    }
                }
            }

            //------------------------------------------------------------------------

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Category_Id"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            ViewData["SubCategory_Id"] = new SelectList(_context.SubCategories, "SubCategory_Id", "SubCategory_Name");

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);

            ViewData["Category_Id"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            ViewData["SubCategory_Id"] = new SelectList(_context.SubCategories, "SubCategory_Id", "SubCategory_Name");

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Product_Id,ProductName,ProductDescription,ProductImage,ProductPrice,Category_Id,SubCategory_Id,Product_Feature")] Product product)
        {
            if (id != product.Product_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Product_Id))
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

            ViewData["Category_Id"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            ViewData["SubCategory_Id"] = new SelectList(_context.SubCategories, "SubCategory_Id", "SubCategory_Name");

            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Product_Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Product_Id == id);
        }
    }
}
