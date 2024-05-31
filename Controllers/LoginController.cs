using Agri_Energy_Application.Models;
using Agri_Energy_Application.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Agri_Energy_App.Controllers
{
    public class LoginController : Controller
    {
        private readonly ArgiEnergyContext _context;
        public LoginController(ArgiEnergyContext context) 
        { 
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                User u = _context.Users.Where(user => user.Email.Equals(email)).FirstOrDefault();
                string currentUser = u.Email;
                string role = u.Role;

                AuthLogger auth = new AuthLogger();

                if (u != null && u.Password == password)
                {
                    if (role == "Farmer")
                    {
                        HttpContext.Session.SetString("LoggedInUser", currentUser);
                        HttpContext.Session.SetString("UserRole", role);

                        auth.LogSuccess(u.Email);
                        return RedirectToAction("Farmers", "Dashboard");
                    }
                    else
                    {
                        HttpContext.Session.SetString("LoggedInUser", currentUser);
                        HttpContext.Session.SetString("UserRole", role);

                        auth.LogSuccess(email);
                        return RedirectToAction("Employees", "Dashboard");
                    }
                }
                else
                {
                    ViewBag.Error = "Username invalid email or password";
                    auth.LogError(email);
                }
                return View();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                ViewBag.Error = "An error occurred: " + ex.Message;
                return View();
            }
        }
    }
}
