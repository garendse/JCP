using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCPBackend.Models;

namespace JCPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteItemSupplierController : ControllerBase
    {
        private readonly JobCostProTestingContext _context;

        public QuoteItemSupplierController(JobCostProTestingContext context)
        {
            _context = context;
        }

        // PUT: api/QuoteItemSupplier/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putj_quote_item_supplier(string id, j_quote_item_supplier j_quote_item_supplier)
        {
            if (id != j_quote_item_supplier.id)
            {
                return BadRequest();
            }

            _context.Entry(j_quote_item_supplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!j_quote_item_supplierExists(id))
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

        // POST: api/QuoteItemSupplier
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Postj_quote_item_supplier(j_quote_item_supplier j_quote_item_supplier)
        {
            j_quote_item_supplier.id = Guid.NewGuid().ToString();

            _context.j_quote_item_suppliers.Add(j_quote_item_supplier);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (j_quote_item_supplierExists(j_quote_item_supplier.id))
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

        // DELETE: api/QuoteItemSupplier/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletej_quote_item_supplier(string id)
        {
            var j_quote_item_supplier = await _context.j_quote_item_suppliers.FindAsync(id);
            if (j_quote_item_supplier == null)
            {
                return NotFound();
            }

            _context.j_quote_item_suppliers.Remove(j_quote_item_supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool j_quote_item_supplierExists(string id)
        {
            return _context.j_quote_item_suppliers.Any(e => e.id == id);
        }
    }
}
