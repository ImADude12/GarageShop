using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageShop.Data;
using GarageShop.Models;
using Microsoft.AspNetCore.Authorization;

namespace GarageShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly GarageShopContext _context;

        public ProductsController(GarageShopContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var garageShopContext = _context.Product.Include(p => p.Category).Include(p => p.Seller);
            return View(await garageShopContext.ToListAsync());
        }
        public async Task<IActionResult> Search(string queryName)
        {
            var searchContext = _context.Product.Include(a => a.Category).Where(a => (a.Name.Contains(queryName) || queryName == null));
            return View("Index", await searchContext.ToListAsync());
        }
        // GET: Products/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
   //         ProductTagView prod = new ProductTagView();
   //         prod.TagsList = _context.Tag.Select(o => new SelectListItem
			//{
			//	Text = o.Name,
			//	Value = o.Id.ToString()
			//});
            //ViewData["Tags"] = new SelectList(_context.Tag, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["SellerId"] = new SelectList(_context.Seller, "Id", "Name");
            //return View(prod);
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Image,Description,Price,SellerId,CategoryId,Tag")] Product product)
        {
           
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["Tags"] = new SelectList(_context.Tag, "Id", "Name",_context.Tag);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            ViewData["SellerId"] = new SelectList(_context.Seller, "Id", "Name", product.SellerId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductTagView prod = new ProductTagView{ Id = product.Id, Name = product.Name, Image = product.Image, Description = product.Description, Price = product.Price, SellerId = product.SellerId, CategoryId = product.CategoryId };
            prod.TagsList = _context.Tag.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Id.ToString()
            });
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            ViewData["SellerId"] = new SelectList(_context.Seller, "Id", "Name", product.SellerId);
            return View(prod);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image,Description,Price,SellerId,CategoryId,TagIds")] ProductTagView product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var prod = _context.Product
                    .Include(i => i.Tag).First(i => i.Id == product.Id);
                    foreach (int TagId in product.TagIds)
                    {
                        prod.Tag.Add(_context.Tag.Where(a => a.Id == TagId).FirstOrDefault());
                    }

                    _context.Update(prod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            ViewData["SellerId"] = new SelectList(_context.Seller, "Id", "Name", product.SellerId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
