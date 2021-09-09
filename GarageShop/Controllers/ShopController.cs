using GarageShop.Data;
using GarageShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GarageShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly GarageShopContext _context;
        private readonly ILogger<ShopController> _logger;


        public ShopController(ILogger<ShopController> logger, GarageShopContext context)
        {
            _context = context;
            _logger = logger;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
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
