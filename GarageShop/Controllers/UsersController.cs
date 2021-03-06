using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageShop.Data;
using GarageShop.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace GarageShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly GarageShopContext _context;
        private readonly CartsController _cartsController;


        public UsersController(GarageShopContext context, CartsController cartsController)
        {
            _context = context;
            _cartsController = cartsController;
        }

        
        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        public async Task<IActionResult> Search(string queryName)
        {
            var searchContext = _context.User.Where(a => (a.Username.Contains(queryName) || queryName == null));
            return View("Index", await searchContext.ToListAsync());
        }
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [AllowAnonymous]
        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        // POST: Users/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Username,Password,FullName,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                var q = _context.User.FirstOrDefault(u => u.Username == user.Username || u.Email == user.Email);
                if (q == null)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    var u = _context.User.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
                    Signin(u);
                    if (!_context.Cart.Any(o => o.UserId == u.Id))
                    {
                        //_context.Cart.
                        Cart cart = new Cart { UserId = u.Id };
                        var res = await _cartsController.Create(cart);
                    }
                    return RedirectToAction(nameof(Index), "Home");
                } else
                {
                    ViewData["Error"] = "Unable to comply; Cannot register this user";
                }
            }
            return View(user);
        }

        [AllowAnonymous]
        // GET: Users/AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,Username,Password")] User user)
        {
            ModelState.Remove("FullName");
            ModelState.Remove("Email");
            if (ModelState.IsValid)
            {
             
                // Regular - 
                //var q = _context.User.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
                // if (q != null)

                // Linq - 
                var q = from u in _context.User
                        where u.Username == user.Username && u.Password == user.Password
                        select u;
                if (q.Count() > 0)
                {
                    ViewData["Error"] = "User and/or password are incorrectasfasfd";
                    Signin(q.First());
                    if (!_context.Cart.Any(o => o.UserId == q.First().Id))
                    {
                        //_context.Cart.
                        Cart cart = new Cart { UserId = q.First().Id};
                        var res = await _cartsController.Create(cart);
                    }
                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    ViewData["Error"] = "User and/or password are incorrect";
                }
            }
            return View(user);
        }

        [AllowAnonymous]
        private async Task Signin(User account)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, account.UserType.ToString()),
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            
            var authProperties = new AuthenticationProperties
            {
                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)
            };
            
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // Creating a cart for user
            
        }

        [AllowAnonymous]
        // GET: Users/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
         
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,FullName,Email,UserType")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
