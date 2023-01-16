using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCPBackend.Models;
using System.Security.Claims;

// Used to list the users & add new users
namespace JCPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly JobCostProTestingContext _context;

        public UsersController(JobCostProTestingContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<j_user>>> Getj_users()
        {
            return await _context.j_users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<j_user>> Getj_user(string id)
        {
            var j_user = await _context.j_users.FindAsync(id);

            if (j_user == null)
            {
                return NotFound();
            }

            return j_user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putj_user(string id, j_user j_user)
        {
            if (id != j_user.id)
            {
                return BadRequest();
            }

            _context.Entry(j_user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!j_userExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<j_user>> Postj_user(j_user j_user)
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            j_user.sites.Add(_context.j_sites.Where(u => u.id == site).First());
            j_user.id = Guid.NewGuid().ToString();

            _context.j_users.Add(j_user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (j_userExists(j_user.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getj_user", new { id = j_user.id }, j_user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletej_user(string id)
        {
            var j_user = await _context.j_users.FindAsync(id);
            if (j_user == null)
            {
                return NotFound();
            }

            _context.j_users.Remove(j_user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool j_userExists(string id)
        {
            return _context.j_users.Any(e => e.id == id);
        }
    }
}
