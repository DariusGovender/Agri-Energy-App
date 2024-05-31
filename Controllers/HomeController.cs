using Agri_Energy_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Agri_Energy_Application.Controllers
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
            string username = HttpContext.Session.GetString("LoggedInUser");
            string role = HttpContext.Session.GetString("loggedUserRole");

            TempData["Username"] = username;
            TempData["Role"] = role;

            ViewBag.User = username;

            return View();
        }

        public IActionResult Login()
        {
            return RedirectToAction("Index", "Login");
        }

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

        public IActionResult Farmer()
        {
            return RedirectToAction("Farmers", "Dashboard");
        }

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
