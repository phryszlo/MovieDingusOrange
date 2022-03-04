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
    public class TV_NetworkController : Controller
    {
        private readonly MovieDbContext _context;

        public TV_NetworkController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: TV_Network
        public async Task<IActionResult> Index()
        {
            return View(await _context.TV_Networks.ToListAsync());
        }

        // GET: TV_Network/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tV_Network = await _context.TV_Networks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tV_Network == null)
            {
                return NotFound();
            }

            return View(tV_Network);
        }

        // GET: TV_Network/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TV_Network/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TV_Network tV_Network)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tV_Network);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tV_Network);
        }

        // GET: TV_Network/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tV_Network = await _context.TV_Networks.FindAsync(id);
            if (tV_Network == null)
            {
                return NotFound();
            }
            return View(tV_Network);
        }

        // POST: TV_Network/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TV_Network tV_Network)
        {
            if (id != tV_Network.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tV_Network);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TV_NetworkExists(tV_Network.Id))
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
            return View(tV_Network);
        }

        // GET: TV_Network/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tV_Network = await _context.TV_Networks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tV_Network == null)
            {
                return NotFound();
            }

            return View(tV_Network);
        }

        // POST: TV_Network/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tV_Network = await _context.TV_Networks.FindAsync(id);
            _context.TV_Networks.Remove(tV_Network);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TV_NetworkExists(int id)
        {
            return _context.TV_Networks.Any(e => e.Id == id);
        }
    }
}
