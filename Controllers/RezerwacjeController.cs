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
    public class RezerwacjeController : Controller
    {
        protected readonly RezerwacjaBoiskaContext _context;

        public RezerwacjeController(RezerwacjaBoiskaContext context)
        {
            _context = context;
        }

        // GET: Rezerwacje
        public async Task<IActionResult> Index()
        {
              return _context.Rezerwacje != null ? 
                          View(await _context.Rezerwacje.ToListAsync()) :
                          Problem("Entity set 'RezerwacjaBoiskaContext.Rezerwacje'  is null.");
        }

        public IActionResult Informacje()
        {
            DateTime dzisiaj = DateTime.Today;
            List<Rezerwacje> rezerwacjeAnulowane = _context.Rezerwacje
                .Where(r => r.Status == StatusRezerwacji.Anulowana)
                .ToList();
            List<Rezerwacje> rezerwacjeWTrakcie = _context.Rezerwacje
                .Where(r => r.Status == StatusRezerwacji.WTrakcie)
                .ToList();
            List<Rezerwacje> rezerwacjeDzisiaj = _context.Rezerwacje
                .Where(r => r.DataRezerwacji.Date == dzisiaj)
                .ToList();


            var model = new
            {
                RezerwacjeAnulowane = rezerwacjeAnulowane,
                RezerwacjeDzisiaj = rezerwacjeDzisiaj,
                RezerwacjeWTrakcie = rezerwacjeWTrakcie
            };

            return View(model);
        }

        // GET: Rezerwacje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rezerwacje == null)
            {
                return NotFound();
            }

            var rezerwacje = await _context.Rezerwacje
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezerwacje == null)
            {
                return NotFound();
            }

            return View(rezerwacje);
        }

        private void PopulateGraczeDropDownList(object selectedGracz = null)
        {
            var wybraneEtaty = from e in _context.Gracz
                                orderby e.Imie
                                select e;
            var res = wybraneEtaty.AsNoTracking();
            ViewBag.EtatyID = new SelectList(res, "Id", "Nazwa", selectedGracz);
        }

        private void PopulateBoiskaDropDownList(object selectedBoisko = null)
        {
            var wybraneEtaty = from e in _context.Boiska
                                orderby e.Nazwa
                                select e;
            var res = wybraneEtaty.AsNoTracking();
            ViewBag.ZespolyID = new SelectList(res, "Id", "Nazwa", selectedBoisko);
        }
        // GET: Rezerwacje/Create
        public IActionResult Create()
        {
            var availableStatuses = Enum.GetNames(typeof(StatusRezerwacji));
            foreach(var stat in availableStatuses)
            {
                Console.WriteLine(stat);
            }
            PopulateGraczeDropDownList();
            PopulateBoiskaDropDownList();
            ViewBag.AvailableStatuses = new SelectList(availableStatuses);
            return View();
        }

        // POST: Rezerwacje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataRezerwacji,GodzinaRozpoczecia,GodzinaZakonczenia,Status")] Rezerwacje rezerwacje,
        IFormCollection form)
        {
            string graczValue = form["Gracz"].ToString();
            string boiskoValue = form["Zespol"].ToString();
            if (ModelState.IsValid)
            {
                Gracz gracz = null;
                if (graczValue != "-1")
                {
                    var ee = _context.Gracz.Where(e => e.Id == int.Parse(graczValue));
                    if (ee.Count() > 0)
                        gracz = ee.First();
                }
                Boiska boisko = null;
                if (boiskoValue != "-1")
                {
                    var ee = _context.Boiska.Where(e => e.Id == int.Parse(boiskoValue));
                    if (ee.Count() > 0)
                        boisko = ee.First();
                }
                rezerwacje.Gracze = gracz;
                rezerwacje.Boiska = boisko;
                _context.Add(rezerwacje);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rezerwacje);
        }

        // GET: Rezerwacje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rezerwacje == null)
            {
                return NotFound();
            }

            var rezerwacje = await _context.Rezerwacje.FindAsync(id);
            if (rezerwacje == null)
            {
                return NotFound();
            }
            return View(rezerwacje);
        }

        // POST: Rezerwacje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataRezrwacji,GodzinaRozpoczecia,GodzinaZakonczenia,Status")] Rezerwacje rezerwacje)
        {
            if (id != rezerwacje.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezerwacje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezerwacjeExists(rezerwacje.Id))
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
            return View(rezerwacje);
        }

        // GET: Rezerwacje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rezerwacje == null)
            {
                return NotFound();
            }

            var rezerwacje = await _context.Rezerwacje
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezerwacje == null)
            {
                return NotFound();
            }

            return View(rezerwacje);
        }

        // POST: Rezerwacje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rezerwacje == null)
            {
                return Problem("Entity set 'RezerwacjaBoiskaContext.Rezerwacje'  is null.");
            }
            var rezerwacje = await _context.Rezerwacje.FindAsync(id);
            if (rezerwacje != null)
            {
                _context.Rezerwacje.Remove(rezerwacje);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezerwacjeExists(int id)
        {
          return (_context.Rezerwacje?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
