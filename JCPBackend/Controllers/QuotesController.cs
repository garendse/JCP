using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCPBackend.Models;
using System.Security.Claims;

// This controller is used for the main quote screen and the detailed quote screen and saving the main quote screen info
namespace JCPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly JobCostProTestingContext _context;

        public QuotesController(JobCostProTestingContext context)
        {
            _context = context;
        }

        // GET: api/JQuotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<j_quote>>> GetJQuotes()
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            var data = await _context.j_quotes
                .Where(u => u.site_access == site)
                .Include("customer")
                .Include("vehicle")
                .Include("tech")
                .ToListAsync();
            return data;
        }

        // GET: api/JQuotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<j_quote>> GetJQuote(string id)
        {
            var jQuote = await _context.j_quotes
                .Where(u => u.id == id)
                .Include("vehicle")
                .Include("customer")
                .Include("items")
                .Include("tech")
                .Include("create_user")
                .Include("update_user")
                .Include("items.subquotes.user")
                .Include("items.subquotes.supplier")
                .Include("items.subquotes.supplier.supplier")
                .FirstOrDefaultAsync();

            if (jQuote == null)
            {
                return NotFound();
            }

            return jQuote;
        }

        // PUT: api/JQuotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJQuote(string id, j_quote jQuote)
        {
            if (id != jQuote.id)
            {
                return BadRequest();
            }

            _context.Entry(jQuote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JQuoteExists(id))
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

        // POST: api/JQuotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<j_quote>> PostJQuote(j_quote quote)
        {
            var rnd = new Random(DateTime.Now.Millisecond);

            if (
                quote.ro_number == ""
                || _context.j_quotes.Where(u => u.ro_number == quote.ro_number).Count() > 0
            )
            {
                do
                {
                    quote.ro_number = rnd.Next(10000, 100_000).ToString();
                } while (_context.j_quotes.Where(u => u.ro_number == quote.ro_number).Count() > 0);
            }

            quote.id = Guid.NewGuid().ToString();

            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            // Generate a UUID because SQL server does not do it automatically
            quote.id = Guid.NewGuid().ToString();
            quote.site_access = site;

            // Everything _should_ be ok to add because this does not use related data to update.
            _context.j_quotes.Add(quote);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JQuoteExists(quote.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJQuote", new { id = quote.id }, quote);
        }

        // DELETE: api/JQuotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJQuote(string id)
        {
            var jQuote = await _context.j_quotes.FindAsync(id);
            if (jQuote == null)
            {
                return NotFound();
            }

            _context.j_quotes.Remove(jQuote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JQuoteExists(string id)
        {
            return _context.j_quotes.Any(e => e.id == id);
        }
    }
}
