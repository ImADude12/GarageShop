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
using System.Security.Claims;

namespace GarageShop.Controllers
{
    [Authorize]
    public class CartsController : Controller
    {
        private readonly GarageShopContext _context;

        public CartsController(GarageShopContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var products = (from cart2 in _context.Cart
                            where cart2.UserId == userId
                         select cart2.Products  ).First();

            //Cart cart = _context.Cart.FirstOrDefault(c => c.UserId == userId);
            ViewBag.Products = products;
            ViewBag.User = _context.User.FirstOrDefault(u => u.Id == userId);
            var sum = 0;
            foreach (Product prod in products)
            {
                sum += prod.Price;
            }
            ViewBag.TotalPrice = sum;
            return View();

        }

        // Http Get - /carts/add_product{id}
        [HttpGet]
        public async Task<IActionResult> Add_Product(int ProdId)
        {
            if (ModelState.IsValid)
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                Cart cart = _context.Cart.FirstOrDefault(c => c.UserId == userId);
                try
                {
                    if (cart.Products == null)
                    {
                        cart.Products = new List<Product>();
                    }
                    Product prod = _context.Product.FirstOrDefault(p => p.Id == ProdId);
                    if (prod != null)
                    {
                        cart.Products.Add(prod);
                        _context.Update(cart);
                        _context.Entry(cart).State = EntityState.Modified;

                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            return View();
        }

        [Authorize(Roles = "Admin")]
        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        [Authorize(Roles = "Admin")]
        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Email");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                if (!_context.Cart.Any(o => o.UserId == cart.UserId))
                {
                    _context.Add(cart);
                    try
                    {
                        var res = await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {

                    }
                    return RedirectToAction(nameof(Index));
                } else
                {

                }
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Email", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Email", cart.UserId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Email", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
