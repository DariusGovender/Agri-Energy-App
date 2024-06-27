using Agri_Energy_Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agri_Energy_Application.Controllers
{
    // Controller for handling dashboard-related actions
    public class DashboardController : Controller
    {
        private readonly ArgiEnergyContext _context;

        // Constructor to initialize database context
        public DashboardController(ArgiEnergyContext context)
        {
            _context = context; // Database context
        }

        // Employee Navigation

        //Displays the employees dashboard
        public IActionResult Employees()
        {
            string username = HttpContext.Session.GetString("LoggedInUser");
            string role = HttpContext.Session.GetString("loggedUserRole");

            TempData["Username"] = username;
            TempData["Role"] = role;

            ViewBag.User = username;

            return View();
        }

        // Back button method for employees
        public async Task<IActionResult> EmployeeBackFunction()
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

        //Displays the farmers dashboard
        public IActionResult Farmers()
        {
            string username = HttpContext.Session.GetString("LoggedInUser");
            string role = HttpContext.Session.GetString("loggedUserRole");

            TempData["Username"] = username;
            TempData["Role"] = role;

            ViewBag.User = username;

            return View();
        }

        // Back button method for farmers
        public async Task<IActionResult> Back()
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
