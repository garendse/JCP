using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using JCP;
using JCP.Filters;

namespace JCP.Controllers
{
    public class CustomersController : BaseApiController
    {
        private JobCostProEntities db = new JobCostProEntities();

        // GET: api/Customers
        [JWTAuth]
        public IQueryable<Object> Getj_customers(string mobile_no = "", int limit = 10, string type = "")
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            return db.j_customers.Where(u => u.site_access_id == site && u.type.Contains(type) && (u.mobile_no.Contains(mobile_no) || u.alt_no.Contains(mobile_no) || u.home_no.Contains(mobile_no) || u.company_name.Contains(mobile_no) || u.work_no.Contains(mobile_no))).OrderBy(c => c.name).Take(limit);
        }

        // GET: api/Customers/5
        [JWTAuth]
        [ResponseType(typeof(j_customers))]
        public IHttpActionResult Getj_customers(string id)
        {
            j_customers j_customers = db.j_customers.Find(id);
            if (j_customers == null)
            {
                return NotFound();
            }

            return Ok(j_customers);
        }

        // PUT: api/Customers/5
        [JWTAuth]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putj_customers(string id, j_customers j_customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != j_customers.id)
            {
                return BadRequest();
            }

            db.Entry(j_customers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!j_customersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = j_customers.id }, j_customers);
        }

        // POST: api/Customers
        [JWTAuth]
        [ResponseType(typeof(j_customers))]
        public IHttpActionResult Postj_customers(j_customers j_customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            j_customers.site_access_id = site;
            j_customers.id = Guid.NewGuid().ToString();

            db.j_customers.Add(j_customers);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (j_customersExists(j_customers.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = j_customers.id }, j_customers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool j_customersExists(string id)
        {
            return db.j_customers.Count(e => e.id == id) > 0;
        }
    }
}