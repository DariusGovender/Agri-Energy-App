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
    public class ProductsController : Controller
    {
        private readonly ArgiEnergyContext _context;

        public ProductsController(ArgiEnergyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Back()
        {
            return RedirectToAction("Farmers", "Dashboard");
        }

        public async Task<IActionResult> EmployeeBackFunction()
        {
            return RedirectToAction("Employees", "Dashboard");
        }

        public async Task<IActionResult> ViewList(string search, DateOnly? startDate, DateOnly? endDate)
        {
            string username = HttpContext.Session.GetString("LoggedInUser");
            string role = HttpContext.Session.GetString("loggedUserRole");

            TempData["Username"] = username;
            TempData["Role"] = role;

            ViewBag.User = username;

            // Fetch the products asynchronously
            var products = await _context.Products.ToListAsync();

            // Filter by search term
            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.ProductName.Contains(search) || p.Category.Contains(search)).ToList();
            }

            // Filter by start date
            if (startDate.HasValue)
            {
                products = products.Where(p => p.ProductionDate >= startDate.Value).ToList();
            }

            // Filter by end date
            if (endDate.HasValue)
            {
                products = products.Where(p => p.ProductionDate <= endDate.Value).ToList();
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                products = products.Where(p => p.ProductionDate >= startDate.Value && p.ProductionDate <= endDate.Value).ToList();
            }

            return View(products);
        }

        // GET: Products
        public async Task<IActionResult> Index(string search)
        {
            string username = HttpContext.Session.GetString("LoggedInUser");
            string role = HttpContext.Session.GetString("loggedUserRole");

            TempData["Username"] = username;
            TempData["Role"] = role;

            ViewBag.User = username;

            var farmerId = _context.Farmers.Where(f => f.Email == username).Select(f => f.FarmerId).FirstOrDefault();

            var argiEnergyProducts = _context.Products.Where(p => p.FarmerId == farmerId);

            if (string.IsNullOrEmpty(search))
            {
                return View(await argiEnergyProducts.ToListAsync());
            }
            else
            {
                return View(await argiEnergyProducts.Where(p => p.ProductName.Contains(search) || p.Category.Contains(search)).ToListAsync());
            }
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["FarmerId"] = new SelectList(_context.Farmers, "FarmerId", "FarmerId");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Category,ProductionDate")] Product product)
        {
            string username = HttpContext.Session.GetString("LoggedInUser");
            string role = HttpContext.Session.GetString("loggedUserRole");

            TempData["Username"] = username;
            TempData["Role"] = role;

            var farmerId = _context.Farmers.Where(f => f.Email == username).Select(f => f.FarmerId).FirstOrDefault();

            if (ModelState.IsValid)
            {
                var pro = new Product
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Category = product.Category,
                    ProductionDate = product.ProductionDate,
                    FarmerId = farmerId,
                };

                _context.Add(pro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FarmerId"] = new SelectList(_context.Farmers, "FarmerId", "FarmerId", product.FarmerId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
