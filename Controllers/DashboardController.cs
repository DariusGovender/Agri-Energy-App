using Agri_Energy_Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agri_Energy_Application.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ArgiEnergyContext _context;

        public DashboardController(ArgiEnergyContext context)
        {
            _context = context;
        }

        // Employee Navigation
        public IActionResult Employees()
        {
            string username = HttpContext.Session.GetString("LoggedInUser");
            string role = HttpContext.Session.GetString("loggedUserRole");

            TempData["Username"] = username;
            TempData["Role"] = role;

            ViewBag.User = username;

            return View();
        }

        public IActionResult BackEmployee()
        {
            return RedirectToAction("Employees", "Dashboard");
        }

        public IActionResult AddFarmerFunction() 
        {
            return RedirectToAction("Create", "Users");
        }

        public IActionResult ViewList()
        {
            return RedirectToAction("ViewList", "Products");
        }

        // Farmer Navigation

        public IActionResult Farmers()
        {
            string username = HttpContext.Session.GetString("LoggedInUser");
            string role = HttpContext.Session.GetString("loggedUserRole");

            TempData["Username"] = username;
            TempData["Role"] = role;

            ViewBag.User = username;

            return View();
        }

        public IActionResult BackFarmer()
        {
            return RedirectToAction("Farmers", "Dashboard");
        }

        public IActionResult ViewProducts()
        {
            return RedirectToAction("Index", "Products");
        }

        public IActionResult AddProduct()
        {
            return RedirectToAction("Create", "Products");
        }

    }
}
