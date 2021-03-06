﻿using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment3Client.Models;

namespace Assignment3Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Index(string adminLoginID, string password)
        {
            if(adminLoginID == "admin" && password == "admin")
            {
                // Session to check if admin is logged in 
                HttpContext.Session.SetString(nameof(AdminLogin.AdminLoginID), adminLoginID);

                return RedirectToAction("Index", "Admins");
            }

            ModelState.AddModelError("LoginFailed", "Login failed please try again");

            return View(new AdminLogin { AdminLoginID = adminLoginID });
        }

        public IActionResult Logout()
        {
            // Log out admin
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
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
