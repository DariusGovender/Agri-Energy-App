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
    public class FarmersController : Controller
    {
        private readonly ArgiEnergyContext _context;

        public FarmersController(ArgiEnergyContext context)
        {
            _context = context;
        }

        // GET: Farmers
        public async Task<IActionResult> Index()
        {
            var argiEnergyContext = _context.Farmers.Include(f => f.EmailNavigation);
            return View(await argiEnergyContext.ToListAsync());
        }

        public async Task<IActionResult> BackToDash()
        {
            return RedirectToAction("Employees", "Dashboard");
        }

        public async Task<IActionResult> BackToUser()
        {
            return RedirectToAction("Create", "Users");
        }

        // GET: Farmers/Create
        public IActionResult Create()
        {
            string username = HttpContext.Session.GetString("LoggedInUser");
            string role = HttpContext.Session.GetString("loggedUserRole");

            string farmerUser = HttpContext.Session.GetString("createdUser");

            TempData["createdUser"] = farmerUser;
            TempData["Username"] = username;
            TempData["Role"] = role;

            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email");
            return View();
        }

        // POST: Farmers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FarmerId,Email,FullName,ContactNumber,Address")] Farmer farmer)
        {
            string username = HttpContext.Session.GetString("LoggedInUser");
            string role = HttpContext.Session.GetString("loggedUserRole");
            string farmerUser = HttpContext.Session.GetString("createdUser");

            TempData["createdUser"] = farmerUser;
            TempData["Username"] = username;
            TempData["Role"] = role;

            if (ModelState.IsValid)
            {
                var newFarmer = new Farmer
                { 
                    FarmerId = farmer.FarmerId,
                    Email = farmerUser,
                    FullName = farmer.FullName,
                    ContactNumber = farmer.ContactNumber,
                    Address = farmer.Address,
                };

                _context.Add(newFarmer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Employees","Dashboard");
            }
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email", farmer.Email);
            return View(farmer);
        }

        private bool FarmerExists(int id)
        {
            return _context.Farmers.Any(e => e.FarmerId == id);
        }
    }
}
