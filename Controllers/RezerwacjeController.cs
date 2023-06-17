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
            var rezerwacja = _context.Rezerwacje.Include(p => p.Boiska).Include(p => p.Gracze).AsNoTracking();
              return _context.Rezerwacje != null ? 
                          View(await rezerwacja.ToListAsync()) :
                          Problem("Entity set 'RezerwacjaBoiskaContext.Rezerwacje'  is null.");
        }

        public IActionResult Informacje()
        {
            DateTime dzisiaj = DateTime.Today;
            List<Rezerwacje> rezerwacjeAnulowane = _context.Rezerwacje.Include(p => p.Boiska).Include(p => p.Gracze)
                .Where(r => r.Status == StatusRezerwacji.Anulowana)
                .ToList();
            List<Rezerwacje> rezerwacjeWTrakcie = _context.Rezerwacje.Include(p => p.Boiska).Include(p => p.Gracze)
                .Where(r => r.Status == StatusRezerwacji.WTrakcie)
                .ToList();
            List<Rezerwacje> rezerwacjeDzisiaj = _context.Rezerwacje.Include(p => p.Boiska).Include(p => p.Gracze)
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
                .Include(p => p.Gracze)
                .Include(p => p.Boiska)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezerwacje == null)
            {
                return NotFound();
            }

            return View(rezerwacje);
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
        // GET: Rezerwacje/Create
        public IActionResult Create()
        {
            var availableStatuses = Enum.GetNames(typeof(StatusRezerwacji));
            ViewBag.AvailableStatuses = new SelectList(availableStatuses);
            PopulateGraczeDropDownList();
            PopulateBoiskaDropDownList();
            return View();
        }

        // POST: Rezerwacje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataRezerwacji,GodzinaRozpoczecia,GodzinaZakonczenia,Status")] 
        Rezerwacje rezerwacje,
        IFormCollection form)
        {
            string graczValue = form["Gracze"].ToString();
            string boiskoValue = form["Boiska"].ToString();
            Console.WriteLine(graczValue);
            Console.WriteLine(boiskoValue);
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
            var availableStatuses = Enum.GetNames(typeof(StatusRezerwacji));
            ViewBag.AvailableStatuses = new SelectList(availableStatuses);
            if (id == null || _context.Rezerwacje == null)
            {
                return NotFound();
            }

            var rezerwacja = _context.Rezerwacje.Where(p => p.Id == id)
                .Include(p => p.Gracze).Include(p => p.Boiska).First();
            if (rezerwacja == null)
            {
                return NotFound();
            }
            if(rezerwacja.Gracze != null)
            {
                PopulateGraczeDropDownList(rezerwacja.Gracze.Id);
            }
            else
            {
                PopulateGraczeDropDownList();
            }

            if(rezerwacja.Boiska != null)
            {
                PopulateBoiskaDropDownList(rezerwacja.Boiska.Id);
            }
            else
            {
                PopulateBoiskaDropDownList();
            }
            return View(rezerwacja);
        }

        // POST: Rezerwacje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataRezrwacji,GodzinaRozpoczecia,GodzinaZakonczenia,Status")] Rezerwacje rezerwacje,
        IFormCollection form)
        {
            if (id != rezerwacje.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    String graczValue = form["Gracze"];
                    String boiskoValue = form["Boiska"];
                    Gracz gracz = null;
                    if(graczValue != "-1")
                    {
                        var ee = _context.Gracz.Where(e => e.Id == int.Parse(graczValue));
                        if(ee.Count() > 0)
                        {
                            gracz = ee.First();
                        }
                    }
                    Boiska boisko = null;
                    if(boiskoValue != "-1")
                    {
                        var ee = _context.Boiska.Where(e => e.Id == int.Parse(boiskoValue));
                        if(ee.Count() > 0)
                        {
                            boisko = ee.First();
                        }
                    }

                    rezerwacje.Gracze = gracz;
                    rezerwacje.Boiska = boisko;

                    Rezerwacje pp = _context.Rezerwacje.Where(p => p.Id == id)
                    .Include(p => p.Boiska)
                    .Include(p => p.Gracze)
                    .First();
                    pp.Gracze = gracz;
                    pp.Boiska = boisko;
                    pp.DataRezerwacji = rezerwacje.DataRezerwacji;
                    pp.GodzinaRozpoczecia = rezerwacje.GodzinaRozpoczecia;
                    pp.GodzinaZakonczenia = rezerwacje.GodzinaZakonczenia;
                    pp.Status = rezerwacje.Status;

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

            var rezerwacja = _context.Rezerwacje.Where(p => p.Id == id)
                .Include(p => p.Gracze).Include(p => p.Boiska).First();
            if (rezerwacja == null)
            {
                return NotFound();
            }

            return View(rezerwacja);
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
