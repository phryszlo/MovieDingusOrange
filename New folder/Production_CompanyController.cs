#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieDbApi.Data;

namespace MovieDbApi.Controllers
{
    public class Production_CompanyController : Controller
    {
        private readonly MovieDbContext _context;

        public Production_CompanyController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: Production_Company
        public async Task<IActionResult> Index()
        {
            return View(await _context.Production_Companies.ToListAsync());
        }

        // GET: Production_Company/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production_Company = await _context.Production_Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (production_Company == null)
            {
                return NotFound();
            }

            return View(production_Company);
        }

        // GET: Production_Company/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Production_Company/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Production_Company production_Company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(production_Company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(production_Company);
        }

        // GET: Production_Company/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production_Company = await _context.Production_Companies.FindAsync(id);
            if (production_Company == null)
            {
                return NotFound();
            }
            return View(production_Company);
        }

        // POST: Production_Company/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Production_Company production_Company)
        {
            if (id != production_Company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(production_Company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Production_CompanyExists(production_Company.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(production_Company);
        }

        // GET: Production_Company/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production_Company = await _context.Production_Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (production_Company == null)
            {
                return NotFound();
            }

            return View(production_Company);
        }

        // POST: Production_Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var production_Company = await _context.Production_Companies.FindAsync(id);
            _context.Production_Companies.Remove(production_Company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Production_CompanyExists(int id)
        {
            return _context.Production_Companies.Any(e => e.Id == id);
        }
    }
}
