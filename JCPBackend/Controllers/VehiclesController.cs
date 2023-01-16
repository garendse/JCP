using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCPBackend.Models;
using System.Security.Claims;

// Used to index vehicles
namespace JCPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly JobCostProTestingContext _context;

        public VehiclesController(JobCostProTestingContext context)
        {
            _context = context;
        }

        // GET: api/j_vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<j_vehicle>>> Getj_vehicles(
            string customer_id = "",
            string reg = ""
        )
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            return await _context.j_vehicles
                .Where(
                    u =>
                        u.site_access_id == site
                        && (u.customer_id.Contains(customer_id) && u.registration.Contains(reg))
                )
                .ToListAsync();
        }

        // GET: api/j_vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<j_vehicle>> Getj_vehicle(string id)
        {
            var j_vehicle = await _context.j_vehicles.FindAsync(id);

            if (j_vehicle == null)
            {
                return NotFound();
            }

            return j_vehicle;
        }

        // PUT: api/j_vehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putj_vehicle(string id, j_vehicle j_vehicle)
        {
            if (id != j_vehicle.id)
            {
                return BadRequest();
            }

            _context.Entry(j_vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!j_vehicleExists(id))
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

        // POST: api/j_vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<j_vehicle>> Postj_vehicle(j_vehicle j_vehicle)
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            j_vehicle.site_access_id = site;
            j_vehicle.id = Guid.NewGuid().ToString();

            j_vehicle.id = Guid.NewGuid().ToString();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.j_vehicles.Add(j_vehicle);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (j_vehicleExists(j_vehicle.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getj_vehicle", new { id = j_vehicle.id }, j_vehicle);
        }

        // DELETE: api/j_vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletej_vehicle(string id)
        {
            var j_vehicle = await _context.j_vehicles.FindAsync(id);
            if (j_vehicle == null)
            {
                return NotFound();
            }

            _context.j_vehicles.Remove(j_vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool j_vehicleExists(string id)
        {
            return _context.j_vehicles.Any(e => e.id == id);
        }
    }
}
