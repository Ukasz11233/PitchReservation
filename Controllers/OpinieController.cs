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

        private void PopulateGraczeDropDownList(object selectedGracz = null)
        {
            var wybraniGracze = from e in _context.Gracz
                                orderby e.Imie
                                select e;
            var res = wybraniGracze.AsNoTracking();
            ViewBag.GraczeID = new SelectList(res, "Id", "Imie", selectedGracz);
        }

        private void PopulateBoiskaDropDownList(object selectedBoisko = null)
        {
            var wybraneBoiska = from e in _context.Boiska
                                orderby e.Nazwa
                                select e;
            var res = wybraneBoiska.AsNoTracking();
            ViewBag.BoiskaID = new SelectList(res, "Id", "Nazwa", selectedBoisko);
        }
        // GET: Opinie/Create
        public IActionResult Create()
        {
            var availableGrades = Enum.GetNames(typeof(OcenaBoiska));
            ViewBag.availableGrades = new SelectList(availableGrades);
            PopulateGraczeDropDownList();
            PopulateBoiskaDropDownList();
            return View();
        }

        // POST: Opinie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ocena,Komentarz,DataDodania")] Opinie opinie,
        IFormCollection form)
        {
            string autorValue = form["Autor"].ToString();
            string boiskoValue = form["Boisko"].ToString();
            if (ModelState.IsValid)
            {
                Gracz autor = null;
                if (autorValue != "-1")
                {
                    var ee = _context.Gracz.Where(e => e.Id == int.Parse(autorValue));
                    if (ee.Count() > 0)
                        autor = ee.First();
                }
                Boiska boisko = null;
                if (boiskoValue != "-1")
                {
                    var ee = _context.Boiska.Where(e => e.Id == int.Parse(boiskoValue));
                    if (ee.Count() > 0)
                        boisko = ee.First();
                }
                opinie.Autor = autor;
                opinie.Boisko = boisko;
                opinie.DataDodania = DateTime.Today;

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
