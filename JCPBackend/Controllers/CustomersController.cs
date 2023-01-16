using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCPBackend.Models;
using System.Security.Policy;
using System.Security.Claims;

// This controller is used to get the customers for both chekin and admin
namespace JCPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly JobCostProTestingContext _context;

        public CustomersController(JobCostProTestingContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<j_customer>>> Getj_customers(
            string mobile_no = "",
            int limit = 10
        )
        {
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            return await _context.j_customers
                .Where(
                    u =>
                        u.site_access_id == site
                        && (
                            u.mobile_no.Contains(mobile_no)
                            || u.alt_no.Contains(mobile_no)
                            || u.home_no.Contains(mobile_no)
                            || u.company_name.Contains(mobile_no)
                            || u.work_no.Contains(mobile_no)
                        )
                )
                .OrderBy(c => c.name)
                .Take(limit)
                .ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<j_customer>> Getj_customer(string id)
        {
            var j_customer = await _context.j_customers.FindAsync(id);

            if (j_customer == null)
            {
                return NotFound();
            }

            return j_customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putj_customer(string id, j_customer j_customer)
        {
            if (id != j_customer.id)
            {
                return BadRequest();
            }

            _context.Entry(j_customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!j_customerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<j_customer>> Postj_customer(j_customer j_customer)
        {
            j_customer.id = Guid.NewGuid().ToString();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.j_customers.Add(j_customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (j_customerExists(j_customer.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getj_customer", new { id = j_customer.id }, j_customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletej_customer(string id)
        {
            var j_customer = await _context.j_customers.FindAsync(id);
            if (j_customer == null)
            {
                return NotFound();
            }

            _context.j_customers.Remove(j_customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool j_customerExists(string id)
        {
            return _context.j_customers.Any(e => e.id == id);
        }
    }
}
