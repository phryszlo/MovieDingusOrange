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
    public class TV_SeriesController : Controller
    {
        private readonly MovieDbContext _context;

        public TV_SeriesController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: TV_Series
        public async Task<IActionResult> Index()
        {
            return View(await _context.TV_Serieses.ToListAsync());
        }

        // GET: TV_Series/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tV_Series = await _context.TV_Serieses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tV_Series == null)
            {
                return NotFound();
            }

            return View(tV_Series);
        }

        // GET: TV_Series/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TV_Series/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Popularity")] TV_Series tV_Series)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tV_Series);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tV_Series);
        }

        // GET: TV_Series/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tV_Series = await _context.TV_Serieses.FindAsync(id);
            if (tV_Series == null)
            {
                return NotFound();
            }
            return View(tV_Series);
        }

        // POST: TV_Series/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Popularity")] TV_Series tV_Series)
        {
            if (id != tV_Series.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tV_Series);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TV_SeriesExists(tV_Series.Id))
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
            return View(tV_Series);
        }

        // GET: TV_Series/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tV_Series = await _context.TV_Serieses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tV_Series == null)
            {
                return NotFound();
            }

            return View(tV_Series);
        }

        // POST: TV_Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tV_Series = await _context.TV_Serieses.FindAsync(id);
            _context.TV_Serieses.Remove(tV_Series);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TV_SeriesExists(int id)
        {
            return _context.TV_Serieses.Any(e => e.Id == id);
        }
    }
}
