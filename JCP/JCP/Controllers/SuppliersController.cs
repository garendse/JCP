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
    public class SuppliersController : BaseApiController
    {
        private JobCostProEntities db = new JobCostProEntities();

        // GET: api/Suppliers
        [JWTAuth]
        public IQueryable<j_suppliers> Getj_suppliers()
        {
            return db.j_suppliers;
        }

        // GET: api/Suppliers/5
        [JWTAuth]
        [ResponseType(typeof(j_suppliers))]
        public IHttpActionResult Getj_suppliers(string id)
        {
            j_suppliers j_suppliers = db.j_suppliers.Find(id);
            if (j_suppliers == null)
            {
                return NotFound();
            }

            return Ok(j_suppliers);
        }

        // PUT: api/Suppliers/5
        [JWTAuth]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putj_suppliers(string id, j_suppliers j_suppliers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != j_suppliers.id)
            {
                return BadRequest();
            }

            db.Entry(j_suppliers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!j_suppliersExists(id))
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

        // POST: api/Suppliers
        [JWTAuth]
        [ResponseType(typeof(j_suppliers))]
        public IHttpActionResult Postj_suppliers(j_suppliers supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            supplier.id = Guid.NewGuid().ToString();

            db.j_suppliers.Add(supplier);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (j_suppliersExists(supplier.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = supplier.id }, supplier);
        }

        // DELETE: api/Suppliers/5
        [ResponseType(typeof(j_suppliers))]
        /*public IHttpActionResult Deletej_suppliers(string id)
        {
            j_suppliers j_suppliers = db.j_suppliers.Find(id);
            if (j_suppliers == null)
            {
                return NotFound();
            }

            db.j_suppliers.Remove(j_suppliers);
            db.SaveChanges();

            return Ok(j_suppliers);
        }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool j_suppliersExists(string id)
        {
            return db.j_suppliers.Count(e => e.id == id) > 0;
        }
    }
}