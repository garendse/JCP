using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCPBackend.Models;
using System.Security.Claims;

// Used for technicians
namespace JCPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechsController : ControllerBase
    {
        private readonly JobCostProTestingContext _context;

        public TechsController(JobCostProTestingContext context)
        {
            _context = context;
        }

        // GET: api/Techs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<j_tech>>> GetJTechs()
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            return await _context.j_techs.Where(u => u.site_access_id == site).ToListAsync();
        }

        // PUT: api/Techs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJTech(string id, j_tech jTech)
        {
            if (id != jTech.id)
            {
                return BadRequest();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JTechExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Techs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<j_tech>> PostJTech(j_tech jTech)
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            jTech.site_access_id = site;
            jTech.id = Guid.NewGuid().ToString();

            _context.j_techs.Add(jTech);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JTechExists(jTech.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJTech", new { id = jTech.id }, jTech);
        }

        // DELETE: api/Techs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJTech(string id)
        {
            var jTech = await _context.j_techs.FindAsync(id);
            if (jTech == null)
            {
                return NotFound();
            }

            _context.j_techs.Remove(jTech);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JTechExists(string id)
        {
            return _context.j_techs.Any(e => e.id == id);
        }
    }
}
