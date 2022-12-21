using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCPBackend.Models;
using System.Security.Claims;

namespace JCPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCodesController : ControllerBase
    {
        private readonly JobCostProTestingContext _context;

        public JobCodesController(JobCostProTestingContext context)
        {
            _context = context;
        }

        // GET: api/JobCodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<j_job_code>>> Getj_job_codes()
        {
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            return await _context.j_job_codes.Where(u => u.site_access_id == site).ToListAsync();
        }

        // GET: api/JobCodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<j_job_code>> Getj_job_code(string id)
        {
            var j_job_code = await _context.j_job_codes.FindAsync(id);

            if (j_job_code == null)
            {
                return NotFound();
            }

            return j_job_code;
        }

        // PUT: api/JobCodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putj_job_code(string id, j_job_code j_job_code)
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            if (id != j_job_code.code || j_job_code.code == "")
            {
                return BadRequest();
            }

            j_job_code.site_access_id = site;
            _context.Entry(j_job_code).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!j_job_codeExists(id))
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

        // POST: api/JobCodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<j_job_code>> Postj_job_code(j_job_code j_job_code)
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            j_job_code.site_access_id = site;

            _context.j_job_codes.Add(j_job_code);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (j_job_codeExists(j_job_code.code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getj_job_code", new { id = j_job_code.code }, j_job_code);
        }

        // DELETE: api/JobCodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletej_job_code(string id)
        {
            var j_job_code = await _context.j_job_codes.FindAsync(id);
            if (j_job_code == null)
            {
                return NotFound();
            }

            _context.j_job_codes.Remove(j_job_code);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool j_job_codeExists(string id)
        {
            return _context.j_job_codes.Any(e => e.code == id);
        }
    }
}
