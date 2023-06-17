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
    public class GraczController : Controller
    {
        private readonly RezerwacjaBoiskaContext _context;

        public GraczController(RezerwacjaBoiskaContext context)
        {
            _context = context;
        }

        // GET: Gracz
        public async Task<IActionResult> Index()
        {
              return _context.Gracz != null ? 
                          View(await _context.Gracz.ToListAsync()) :
                          Problem("Entity set 'RezerwacjaBoiskaContext.Gracz'  is null.");
        }

        // GET: Gracz/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gracz == null)
            {
                return NotFound();
            }

            var gracz = await _context.Gracz
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gracz == null)
            {
                return NotFound();
            }

            return View(gracz);
        }

        // GET: Gracz/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gracz/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,Adres,NumerTelefonu,Email")] Gracz gracz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gracz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gracz);
        }

        // GET: Gracz/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gracz == null)
            {
                return NotFound();
            }

            var gracz = await _context.Gracz.FindAsync(id);
            if (gracz == null)
            {
                return NotFound();
            }
            return View(gracz);
        }

        // POST: Gracz/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,Adres,NumerTelefonu,Email")] Gracz gracz)
        {
            if (id != gracz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gracz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GraczExists(gracz.Id))
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
            return View(gracz);
        }

        // GET: Gracz/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gracz == null)
            {
                return NotFound();
            }

            var gracz = await _context.Gracz
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gracz == null)
            {
                return NotFound();
            }

            return View(gracz);
        }

        // POST: Gracz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Gracz == null)
            {
                return Problem("Entity set 'RezerwacjaBoiskaContext.Gracz'  is null.");
            }
            var gracz = await _context.Gracz.FindAsync(id);
            if (gracz != null)
            {
                _context.Gracz.Remove(gracz);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GraczExists(int id)
        {
          return (_context.Gracz?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
