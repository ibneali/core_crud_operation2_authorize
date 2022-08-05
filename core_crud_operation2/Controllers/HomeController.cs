
using core_crud_operation2.dbconnection;
using core_crud_operation2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace core_crud_operation2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly applicationDbcontext _context;

        public HomeController(applicationDbcontext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var res = _context.customers.ToList();
            return View(res);
        }
        [HttpGet]
        public IActionResult addemp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addemp(Customer obj)
        {
            if (obj.CustomerId > 0)
            {
                _context.customers.Update(obj);
                _context.SaveChanges();
            }
            else
            {
                _context.customers.Add(obj);
                _context.SaveChanges();
            }

            return View("Index");
        }
        public IActionResult edit(int id)
        {
            var detail = _context.customers.FirstOrDefault(x => x.CustomerId == id);

            return View("addemp", detail);
        }
        public IActionResult delete(int id)
        {
            var detail = _context.customers.FirstOrDefault(x => x.CustomerId == id);
            _context.customers.Remove(detail);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("Home/login")]
        [HttpPost]
        [AllowAnonymous]
        
        public IActionResult login(Customer obj)
        {
            var res = _context.customers.Where(x => x.Email == obj.Email).FirstOrDefault();
            if (res == null)
            {
                TempData["wrong"] = "This User Not Valid";
            }
            else
            {
                if (res.Email == obj.Email && res.PhoneNumber == obj.PhoneNumber)
                {
                    var claims = new[] {new Claim (ClaimTypes.Name,res.CustomerName),
                new Claim(ClaimTypes.Email,res.Email)};

                    var identity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity));
                    HttpContext.Session.SetString("Name", res.CustomerName);
                   
                    return RedirectToAction("Index");
                }
                TempData["invalid"] = " wrong password or Email";           
            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult login()
        {
            return View();

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
           
            return View("login");

        }
    }
}
