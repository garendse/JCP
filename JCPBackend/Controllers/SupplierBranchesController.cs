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
    public class SupplierBranchesController : ControllerBase
    {
        private readonly JobCostProTestingContext _context;

        public SupplierBranchesController(JobCostProTestingContext context)
        {
            _context = context;
        }

        // GET: api/SupplierBranches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<j_supplier_branch>>> GetJSupplierBranches()
        {
            return await _context.j_supplier_branches.Include("supplier").ToListAsync();
        }

        // GET: api/SupplierBranches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<j_supplier_branch>> GetJSupplierBranch(string id)
        {
            var jSupplierBranch = await _context.j_supplier_branches.FindAsync(id);

            if (jSupplierBranch == null)
            {
                return NotFound();
            }

            return jSupplierBranch;
        }

        // PUT: api/SupplierBranches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJSupplierBranch(string id, j_supplier_branch jSupplierBranch)
        {
            if (id != jSupplierBranch.id)
            {
                return BadRequest();
            }

            _context.Entry(jSupplierBranch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JSupplierBranchExists(id))
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

        // POST: api/SupplierBranches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<j_supplier_branch>> PostJSupplierBranch(j_supplier_branch jSupplierBranch)
        {
            jSupplierBranch.id = Guid.NewGuid().ToString();
            _context.j_supplier_branches.Add(jSupplierBranch);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JSupplierBranchExists(jSupplierBranch.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJSupplierBranch", new { id = jSupplierBranch.id }, jSupplierBranch);
        }

        // DELETE: api/SupplierBranches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJSupplierBranch(string id)
        {
            var jSupplierBranch = await _context.j_supplier_branches.FindAsync(id);
            if (jSupplierBranch == null)
            {
                return NotFound();
            }

            _context.j_supplier_branches.Remove(jSupplierBranch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JSupplierBranchExists(string id)
        {
            return _context.j_supplier_branches.Any(e => e.id == id);
        }
    }
}
