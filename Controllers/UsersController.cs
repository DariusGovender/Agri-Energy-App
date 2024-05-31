using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agri_Energy_Application.Models;

namespace Agri_Energy_Application.Controllers
{
    public class UsersController : Controller
    {
        private readonly ArgiEnergyContext _context;

        public UsersController(ArgiEnergyContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        public async Task<IActionResult> EmployeeBackFunction()
        {
            return RedirectToAction("Employees", "Dashboard");
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password,Role")] User user)
        {
            string username = HttpContext.Session.GetString("LoggedInUser");
            string role = HttpContext.Session.GetString("loggedUserRole");

            TempData["Username"] = username;
            TempData["Role"] = role;
            bool exsistingUser = _context.Users.Any(u => u.Email == user.Email);

            if (ModelState.IsValid)
            {
                if (exsistingUser == false)
                {
                    HttpContext.Session.SetString("createdUser", user.Email);

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create", "Farmers");
                }
                else 
                {
                    ModelState.AddModelError("Email", "Email already exsists");
                }
                    
            }
            return View(user);
        }
        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Email == id);
        }
    }
}
