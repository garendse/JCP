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
    public class TechsController : BaseApiController
    {
        private JobCostProEntities db = new JobCostProEntities();

        // GET: api/Techs
        [JWTAuth]
        public IQueryable<j_techs> Getj_techs()
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            return db.j_techs.Where(u => u.site_access_id == site);
        }

        // PUT: api/Techs/5
        [JWTAuth]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putj_techs(string id, j_techs j_techs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != j_techs.id)
            {
                return BadRequest();
            }

            db.Entry(j_techs).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!j_techsExists(id))
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

        // POST: api/Techs
        [JWTAuth]
        [ResponseType(typeof(j_techs))]
        public IHttpActionResult Postj_techs(j_techs j_techs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            j_techs.site_access_id = site;
            j_techs.id = Guid.NewGuid().ToString();

            db.j_techs.Add(j_techs);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (j_techsExists(j_techs.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = j_techs.id }, j_techs);
        }

        // DELETE: api/Techs/5
        /*[JWTAuth]
        [ResponseType(typeof(j_techs))]
        public IHttpActionResult Deletej_techs(string id)
        {
            j_techs j_techs = db.j_techs.Find(id);
            if (j_techs == null)
            {
                return NotFound();
            }

            db.j_techs.Remove(j_techs);
            db.SaveChanges();

            return Ok(j_techs);
        }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool j_techsExists(string id)
        {
            return db.j_techs.Count(e => e.id == id) > 0;
        }
    }
}