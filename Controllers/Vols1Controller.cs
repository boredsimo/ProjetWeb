using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetWeb.Data;
using ProjetWeb.Models;

namespace ProjetWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Vols1Controller : ControllerBase
    {
        private readonly ProjetWebContext _context;

        public Vols1Controller(ProjetWebContext context)
        {
            _context = context;
        }

        // GET: api/Vols1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vol>>> GetVol()
        {
          if (_context.Vol == null)
          {
              return NotFound();
          }
            return await _context.Vol.ToListAsync();
        }

        // GET: api/Vols1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vol>> GetVol(int id)
        {
          if (_context.Vol == null)
          {
              return NotFound();
          }
            var vol = await _context.Vol.FindAsync(id);

            if (vol == null)
            {
                return NotFound();
            }

            return vol;
        }

        // PUT: api/Vols1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVol(int id, Vol vol)
        {
            if (id != vol.Id)
            {
                return BadRequest();
            }

            _context.Entry(vol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("PutVol",id,vol);
        }

        // POST: api/Vols1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vol>> PostVol(Vol vol)
        {
          
            _context.Vol.Add(vol);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVol", new { id = vol.Id }, vol);
        }

        // DELETE: api/Vols1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVol(int id)
        {
            
            var vol = await _context.Vol.FindAsync(id);
            if (vol == null)
            {
                return NotFound();
            }

            _context.Vol.Remove(vol);
            await _context.SaveChangesAsync();

            return CreatedAtAction("DeleteVol", id, vol);
        }

        private bool VolExists(int id)
        {
            return (_context.Vol.Any(e => e.Id == id));
        }
    }
}
