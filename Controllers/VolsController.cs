using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using ProjetWeb.Data;
using ProjetWeb.Models;
using ProjetWeb.Hubs;

namespace ProjetWeb.Controllers
{
    public class VolsController : Controller
    {
        private readonly ProjetWebContext _context;
        private readonly IHubContext<VolHub> _hubContext;

        public VolsController(ProjetWebContext context, IHubContext<VolHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: Vols
        public async Task<IActionResult> Index()
        {
            var query = from vol in _context.Set<Vol>()
                        join evenement in _context.Set<Evenement>()
                        on vol.Id equals evenement.IDVol
                        select new VolEvenement
                        { Vol = vol, Evenement = evenement };

            return //_context.Vol != null ? 
                          View(await _context.Vol.ToListAsync());
                          //Problem("Entity set 'ProjetWebContext.Vol'  is null.");
        }

        //GET: Vols/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vol == null)
            {
                return NotFound();
            }

            var vol = await _context.Vol
                .Include(e=>e.Evenements)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vol == null)
            {
                return NotFound();
            }

            return View(vol);
        }

        // GET: Vols/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Vols/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Compagnie,CodeVol,Ville,HeurePrevue,HeureRevisee,Statut")] Vol vol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vol);
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("VolChange");
                return RedirectToAction(nameof(Index));
            }
            return View(vol);
        }

        // GET: Vols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vol == null)
            {
                return NotFound();
            }

            var vol = await _context.Vol.FindAsync(id);
            if (vol == null)
            {
                return NotFound();
            }
            return View(vol);
        }

        // POST: Vols/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Compagnie,CodeVol,Ville,HeurePrevue,HeureRevisee,Statut")] Vol vol)
        {
            if (id != vol.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vol);
                    await _context.SaveChangesAsync();
                    await _hubContext.Clients.All.SendAsync("VolChange");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolExists(vol.Id))
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
            return View(vol);
        }

        //GET: Vols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vol == null)
            {
                return NotFound();
            }

            var vol = await _context.Vol
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vol == null)
            {
                return NotFound();
            }
            await _hubContext.Clients.All.SendAsync("VolChange");
            return View(vol);
        }

        // POST: Vols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vol == null)
            {
                return Problem("Entity set 'ProjetWebContext.Vol'  is null.");
            }
            var vol = await _context.Vol.FindAsync(id);
            if (vol != null)
            {
                _context.Vol.Remove(vol);
            }
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("VolChange");
            return RedirectToAction(nameof(Index));
        }

        private bool VolExists(int id)
        {
            return (_context.Vol?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
