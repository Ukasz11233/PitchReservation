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
    public class OpinieController : Controller
    {
        private readonly RezerwacjaBoiskaContext _context;

        public OpinieController(RezerwacjaBoiskaContext context)
        {
            _context = context;
        }

        // GET: Opinie
        public async Task<IActionResult> Index()
        {
            var rezerwacja = _context.Opinie.Include(p => p.Autor).Include(p => p.Boisko).AsNoTracking();
              return _context.Opinie != null ? 
                          View(await rezerwacja.ToListAsync()) :
                          Problem("Entity set 'RezerwacjaBoiskaContext.Opinie'  is null.");
        }

        // GET: Opinie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Opinie == null)
            {
                return NotFound();
            }

            var opinie = await _context.Opinie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opinie == null)
            {
                return NotFound();
            }

            return View(opinie);
        }

        // GET: Opinie/Create
        public IActionResult Create()
        {
            var availableGrades = Enum.GetNames(typeof(OcenaBoiska));
            ViewBag.availableGrades = new SelectList(availableGrades);
            return View();
        }

        // POST: Opinie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ocena,Komentarz,DataDodania")] Opinie opinie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opinie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(opinie);
        }

        // GET: Opinie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Opinie == null)
            {
                return NotFound();
            }

            var opinie = await _context.Opinie.FindAsync(id);
            if (opinie == null)
            {
                return NotFound();
            }
            return View(opinie);
        }

        // POST: Opinie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ocena,Komentarz,DataDodania")] Opinie opinie)
        {
            if (id != opinie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opinie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpinieExists(opinie.Id))
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
            return View(opinie);
        }

        // GET: Opinie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Opinie == null)
            {
                return NotFound();
            }

            var opinie = await _context.Opinie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opinie == null)
            {
                return NotFound();
            }

            return View(opinie);
        }

        // POST: Opinie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Opinie == null)
            {
                return Problem("Entity set 'RezerwacjaBoiskaContext.Opinie'  is null.");
            }
            var opinie = await _context.Opinie.FindAsync(id);
            if (opinie != null)
            {
                _context.Opinie.Remove(opinie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpinieExists(int id)
        {
          return (_context.Opinie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
