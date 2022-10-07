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
    public class JobCodesController : BaseApiController
    {
        private JobCostProEntities db = new JobCostProEntities();

        // GET: api/JobCodes
        [JWTAuth]
        public IQueryable<j_job_codes> Getj_job_codes()
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            return db.j_job_codes.Where(u => u.site_access_id == site);
        }

        // GET: api/JobCodes/5
        [JWTAuth]
        [ResponseType(typeof(j_job_codes))]
        public IHttpActionResult Getj_job_codes(string id)
        {
            j_job_codes j_job_codes = db.j_job_codes.Find(id);
            if (j_job_codes == null)
            {
                return NotFound();
            }

            return Ok(j_job_codes);
        }

        // PUT: api/JobCodes/5
        [JWTAuth]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putj_job_codes(string id, j_job_codes j_job_codes)
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != j_job_codes.code)
            {
                return BadRequest();
            }

            j_job_codes.site_access_id = site;
            db.Entry(j_job_codes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!j_job_codesExists(id))
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

        // POST: api/JobCodes
        [JWTAuth]
        [ResponseType(typeof(j_job_codes))]
        public IHttpActionResult Postj_job_codes(j_job_codes j_job_codes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            j_job_codes.site_access_id = site;
            db.j_job_codes.Add(j_job_codes);


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (j_job_codesExists(j_job_codes.code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = j_job_codes.code }, j_job_codes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool j_job_codesExists(string id)
        {
            return db.j_job_codes.Count(e => e.code == id) > 0;
        }
    }
}