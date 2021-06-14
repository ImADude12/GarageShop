﻿using GarageShop.Data;
using GarageShop.Models;
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
    public class LoginController : Controller
    {
        private readonly GarageShopContext _context;
        private readonly ILogger<LoginController> _logger;


        public LoginController(ILogger<LoginController> logger, GarageShopContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: /Login
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