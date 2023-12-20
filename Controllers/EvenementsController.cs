using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetWeb.Data;
using ProjetWeb.Models;

namespace ProjetWeb.Controllers
{
    public class EvenementsController : Controller
    {
        private readonly ProjetWebContext _context;

        public EvenementsController(ProjetWebContext context)
        {
            _context = context;
        }

        // GET: Evenements
        public async Task<IActionResult> Index()

        {
            //var query = from vol in _context.Set<Vol>()
            //            join evenement in _context.Set<Evenement>()
            //            on vol.Id equals evenement.IDVol
            //            select new VolEvenement
            //            { Vol = vol, Evenement = evenement };
            return _context.Evenement != null ? 
                          View(await _context.Evenement.ToListAsync()):
                          Problem("Entity set 'ProjetWebContext.Evenement'  is null.");
        }

        // GET: Evenements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Evenement == null)
            {
                return NotFound();
            }

            var evenement = await _context.Evenement
                .Include(e=>e.Vol)
                .FirstOrDefaultAsync(m => m.EvenementID == id);
            if (evenement == null)
            {
                return NotFound();
            }

            //var vols = from v in _context.Vol
            //          select v;

            //vols = vols.Where(s => s.Id == evenement.IDVol);

            //evenement.Vol=await vols.ToListAsync();
            //if (evenement.Vol == null)
            //    evenement.Vol = default!;

            return View(evenement);
        }

        // GET: Evenements/Create
        public IActionResult Create()
        {
            ViewData["IDVol"] = new SelectList(_context.Vol, "Id", "Id");
            return View();
        }

        // POST: Evenements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EvenementID,IDVol,HeureRevisee,Statut")] Evenement evenement)
        {
            if (ModelState.IsValid)
            {  
                _context.Add(evenement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDVol"] = new SelectList(_context.Vol, "Id", "Id");
            return View(evenement);
        }

        // GET: Evenements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Evenement == null)
            {
                return NotFound();
            }

            var evenement = await _context.Evenement.FindAsync(id);
            if (evenement == null)
            {
                return NotFound();
            }
            return View(evenement);
        }

        // POST: Evenements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EvenementID,IDVol,HeureRevisee,Statut")] Evenement evenement)
        {
            if (id != evenement.EvenementID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evenement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvenementExists(evenement.EvenementID))
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
            return View(evenement);
        }

        // GET: Evenements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Evenement == null)
            {
                return NotFound();
            }

            var evenement = await _context.Evenement
                .FirstOrDefaultAsync(m => m.EvenementID == id);
            if (evenement == null)
            {
                return NotFound();
            }

            return View(evenement);
        }

        // POST: Evenements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Evenement == null)
            {
                return Problem("Entity set 'ProjetWebContext.Evenement'  is null.");
            }
            var evenement = await _context.Evenement.FindAsync(id);
            if (evenement != null)
            {
                _context.Evenement.Remove(evenement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvenementExists(int id)
        {
          return (_context.Evenement?.Any(e => e.EvenementID == id)).GetValueOrDefault();
        }
    }
}
