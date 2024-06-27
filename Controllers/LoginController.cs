using Agri_Energy_Application.Models;
using Agri_Energy_Application.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Agri_Energy_App.Controllers
{
    // Controller for handling login-related actions
    public class LoginController : Controller
    {
        private readonly ArgiEnergyContext _context;

        // Constructor to initialize database context
        public LoginController(ArgiEnergyContext context) 
        { 
            _context = context; // Database context
        }

        // Default action for displaying login view
        public IActionResult Index()
        {
            return View();
        }

        // Logs out the current user and redirects to the home page
        public IActionResult Home()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // Displays the login view
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Handles user login post requests
        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            try
            {
                // Retrieve user from database based on email
                User u = _context.Users.Where(user => user.Email.Equals(email)).FirstOrDefault();

                AuthLogger auth = new AuthLogger();

                if (u != null && u.Password == password)
                {
                    // Successful login
                    string currentUser = u.Email;
                    string role = u.Role;
                    if (role == "Farmer")
                    {
                        // Set session variables
                        HttpContext.Session.SetString("LoggedInUser", currentUser);
                        HttpContext.Session.SetString("UserRole", role);

                        // Log successful login attempt
                        auth.LogSuccess(u.Email);
                        // Redirect to appropriate dashboard
                        return RedirectToAction("Farmers", "Dashboard");
                    }
                    else
                    {
                        // Set session variables
                        HttpContext.Session.SetString("LoggedInUser", currentUser);
                        HttpContext.Session.SetString("UserRole", role);

                        // Log successful login attempt
                        auth.LogSuccess(email);
                        // Redirect to appropriate dashboard
                        return RedirectToAction("Employees", "Dashboard");
                    }
                }
                else
                {
                    // Invalid login attempt
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
