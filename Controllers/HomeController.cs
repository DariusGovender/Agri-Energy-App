using Agri_Energy_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Agri_Energy_Application.Controllers
{

    // Controller for handling home-related action
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ArgiEnergyContext _context;

        // Constructor to initialize logger and database context.
        public HomeController(ILogger<HomeController> logger, ArgiEnergyContext context)
        {
            _logger = logger; // Logger instance
            _context = context; // Database context
        }

        //  Action method for the home page
        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("LoggedInUser");
            string role = HttpContext.Session.GetString("loggedUserRole");

            TempData["Username"] = username;
            TempData["Role"] = role;

            ViewBag.User = username;

            return View();
        }

        // Redirects to the login page
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Login");
        }

        //Logs out the current user
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("LoggedInUser") != null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "You are not logged in";
                return RedirectToAction("Index", "Home");
            }
        }

        //Redirects to the farmers dashboard page
        public IActionResult Farmer()
        {
            return RedirectToAction("Farmers", "Dashboard");
        }

        //Redirects to the employees dashboard page
        public IActionResult Employees()
        {
            return RedirectToAction("Employees", "Dashboard");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
