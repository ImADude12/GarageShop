using GarageShop.Data;
using GarageShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GarageShop.Controllers
{
    [Authorize(Roles = "Editor,Admin")]
    public class StatisticsController : Controller
    {
        private readonly GarageShopContext _context;
        private readonly ILogger<StatisticsController> _logger;


        public StatisticsController(ILogger<StatisticsController> logger, GarageShopContext context)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
 

            ViewBag.Category = _context.Category;
            ViewBag.Seller = _context.Seller;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Statistics/products_category
        [HttpGet]
        public IEnumerable Products_category()
        {
            var query = (from prod in _context.Product
                         group prod.Name by prod.Category.Name into g
                         select new
                         {
                             CategoryName = g.Key,
                             ProductsCount = g.Count()
                         });
                        

            return query;
        }

        public IEnumerable Products_tags()
        {
            var query = (from tag in _context.Tag
                         select new
                         {
                             Name = tag.Name,
                             ProdCount = tag.Products.Count()
                         });
            return query;
        }
    }
}
