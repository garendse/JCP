using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using JCP;
using JCP.Filters;

namespace JCP.Controllers
{
    public class SupplierBranchesController : BaseApiController
    {
        private JobCostProEntities db = new JobCostProEntities();

        // GET: api/SupplierBranches
        [JWTAuth]
        public IQueryable<j_supplier_branches> Getj_supplier_branches(string branch_id = "")
        {
            return db.j_supplier_branches.Where(u => u.supplier_id.Contains(branch_id));
        }

        // GET: api/SupplierBranches/5
        /*[JWTAuth]
       [ResponseType(typeof(j_supplier_branches))]
       public IHttpActionResult Getj_supplier_branches(string id)
       {
           j_supplier_branches supplier_branch = db.j_supplier_branches.Find(id);
           if (supplier_branch == null)
           {
               return NotFound();
           }

           return Ok(supplier_branch);
       }
       */
        // PUT: api/SupplierBranches/5
        [JWTAuth]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putj_supplier_branches(string id, j_supplier_branches supplier_branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supplier_branch.id)
            {
                return BadRequest();
            }

            db.Entry(supplier_branch).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!j_supplier_branchesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SupplierBranches
        [JWTAuth]
        [ResponseType(typeof(j_supplier_branches))]
        public IHttpActionResult Postj_supplier_branches(j_supplier_branches supplier_branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            supplier_branch.id = Guid.NewGuid().ToString();

            db.j_supplier_branches.Add(supplier_branch);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (j_supplier_branchesExists(supplier_branch.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = supplier_branch.id }, supplier_branch);
        }

        // DELETE: api/SupplierBranches/5
        /*[ResponseType(typeof(j_supplier_branches))]
        public IHttpActionResult Deletej_supplier_branches(string id)
        {
            j_supplier_branches j_supplier_branches = db.j_supplier_branches.Find(id);
            if (j_supplier_branches == null)
            {
                return NotFound();
            }

            db.j_supplier_branches.Remove(j_supplier_branches);
            db.SaveChanges();

            return Ok(j_supplier_branches);
        }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool j_supplier_branchesExists(string id)
        {
            return db.j_supplier_branches.Count(e => e.id == id) > 0;
        }
    }
}