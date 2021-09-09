using GarageShop.Data;
using GarageShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;


namespace GarageShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly GarageShopContext _context;
        private readonly ILogger<HomeController> _logger;
        private static readonly HttpClient client = new HttpClient();


        public HomeController(ILogger<HomeController> logger, GarageShopContext context)
        {
            _context = context;
            _logger = logger;
        }


        // "/home/GETWeatherJSON"
        [HttpGet]
        public IActionResult GetWeatherJSON(float lat, float lng)
        {
            string json = (new WebClient()).DownloadString($"https://api.weatherapi.com/v1/current.json?key=dc5c317d52cd480dbd3185526211506&q={lat},{lng}&aqi=yes");
            return Content(json);
        }
        public IActionResult Index()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
