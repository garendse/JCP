using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCPBackend.Models;
using System.Security.Policy;

namespace JCPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteItemController : ControllerBase
    {
        private readonly JobCostProTestingContext _context;

        public QuoteItemController(JobCostProTestingContext context)
        {
            _context = context;
        }

        // PUT: api/QuoteItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putj_quote_item(string id, j_quote_item j_quote_item)
        {
            if (id != j_quote_item.id)
            {
                return BadRequest();
            }

            _context.Entry(j_quote_item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!j_quote_itemExists(id))
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

        // POST: api/QuoteItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Postj_quote_item(j_quote_item j_quote_item)
        {
            j_quote_item.id = Guid.NewGuid().ToString();

            _context.j_quote_items.Add(j_quote_item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (j_quote_itemExists(j_quote_item.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/QuoteItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletej_quote_item(string id)
        {
            var j_quote_item = await _context.j_quote_items.FindAsync(id);
            if (j_quote_item == null)
            {
                return NotFound();
            }

            _context.j_quote_items.Remove(j_quote_item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool j_quote_itemExists(string id)
        {
            return _context.j_quote_items.Any(e => e.id == id);
        }
    }
}
