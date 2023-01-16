using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCPBackend.Models;

// Used for suppliers
namespace JCPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly JobCostProTestingContext _context;

        public SuppliersController(JobCostProTestingContext context)
        {
            _context = context;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<j_supplier>>> GetJSuppliers()
        {
            return await _context.j_suppliers.Include("supplier").ToListAsync();
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<j_supplier>> GetJSupplier(string id)
        {
            var jSupplier = await _context.j_suppliers.FindAsync(id);

            if (jSupplier == null)
            {
                return NotFound();
            }

            return jSupplier;
        }

        // PUT: api/Suppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJSupplier(string id, j_supplier jSupplier)
        {
            if (id != jSupplier.id)
            {
                return BadRequest();
            }

            _context.Entry(jSupplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JSupplierExists(id))
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

        // POST: api/Suppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<j_supplier>> PostJSupplier(j_supplier jSupplier)
        {
            jSupplier.id = Guid.NewGuid().ToString();
            _context.j_suppliers.Add(jSupplier);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JSupplierExists(jSupplier.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJSupplier", new { id = jSupplier.id }, jSupplier);
        }

        // DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJSupplier(string id)
        {
            var jSupplier = await _context.j_suppliers.FindAsync(id);
            if (jSupplier == null)
            {
                return NotFound();
            }

            _context.j_suppliers.Remove(jSupplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JSupplierExists(string id)
        {
            return _context.j_suppliers.Any(e => e.id == id);
        }
    }
}
