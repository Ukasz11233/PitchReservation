using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RezerwacjaBoiska.Data;
using RezerwacjaBoiska.Models;

namespace RezerwacjaBoiska.Controllers
{
    public class BoiskaController : Controller
    {
        private readonly RezerwacjaBoiskaContext _context;

        public BoiskaController(RezerwacjaBoiskaContext context)
        {
            _context = context;
        }

        // GET: Boiska
        public async Task<IActionResult> Index()
        {
              return _context.Boiska != null ? 
                          View(await _context.Boiska.ToListAsync()) :
                          Problem("Entity set 'RezerwacjaBoiskaContext.Boiska'  is null.");
        }

        // GET: Boiska/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Boiska == null)
            {
                return NotFound();
            }

            var boiska = await _context.Boiska
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boiska == null)
            {
                return NotFound();
            }

            return View(boiska);
        }

        // GET: Boiska/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Boiska/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Lokalizacja,Rozmiar")] Boiska boiska)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boiska);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boiska);
        }

        // GET: Boiska/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Boiska == null)
            {
                return NotFound();
            }

            var boiska = await _context.Boiska.FindAsync(id);
            if (boiska == null)
            {
                return NotFound();
            }
            return View(boiska);
        }

        // POST: Boiska/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Lokalizacja,Rozmiar")] Boiska boiska)
        {
            if (id != boiska.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boiska);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoiskaExists(boiska.Id))
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
            return View(boiska);
        }

        // GET: Boiska/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Boiska == null)
            {
                return NotFound();
            }

            var boiska = await _context.Boiska
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boiska == null)
            {
                return NotFound();
            }

            return View(boiska);
        }

        // POST: Boiska/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Boiska == null)
            {
                return Problem("Entity set 'RezerwacjaBoiskaContext.Boiska'  is null.");
            }
            var boisko = await _context.Boiska.FindAsync(id);
            if (boisko == null)
            {
                return NotFound();
            }
            bool hasReferences = _context.Rezerwacje.Any(r => r.Boiska.Id == id);
            if (hasReferences)
            {
                TempData["DeleteFailed"] = "Cannot delete the record because it is referenced elsewhere.";
                return RedirectToAction(nameof(Index));
            }
            
            _context.Boiska.Remove(boisko);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoiskaExists(int id)
        {
          return (_context.Boiska?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
