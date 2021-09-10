using GarageShop.Data;
using GarageShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageShop.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private readonly GarageShopContext _context;
        private readonly ILogger<ShopController> _logger;


        public ShopController(ILogger<ShopController> logger, GarageShopContext context)
        {
            _context = context;
            _logger = logger;
        }



        public async Task<IActionResult> Index(string? prodName, int? categoryId, int? sellerId)
        {
            IEnumerable<Models.Product> products = _context.Product.Include(a => a.Category).Include(a=>a.Seller);
            if(prodName != null)
            {
                products = products.Where(p => p.Name.Contains(prodName));
            }
            if(categoryId != null)
            {
                products = products.Where(a => (a.CategoryId.Equals(categoryId)));
            }
            if (sellerId != null)
            {
                products = products.Where(a => (a.SellerId.Equals(sellerId)));
            }

            ViewBag.Products = products;
            ViewBag.Category = _context.Category;
            ViewBag.Seller = _context.Seller;
            return View("Index");
        }

        public async Task<IActionResult> Search(string queryName)
        {
            var searchContext = _context.Product.Include(a => a.Category).Where(a => (a.Name.Contains(queryName) || queryName == null));
            return View("Index", await searchContext.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
