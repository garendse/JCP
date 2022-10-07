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

namespace JCP.Controllers
{
    public class ErrorController : BaseApiController
    {
        private JobCostProEntities db = new JobCostProEntities();

        // GET: api/j_errors
        public IQueryable<j_errors> GetErrors()
        {
            return db.j_errors;
        }

        // GET: api/j_errors/5
        [ResponseType(typeof(j_errors))]
        public IHttpActionResult Getj_errors(string id)
        {
            j_errors j_errors = db.j_errors.Find(id);
            if (j_errors == null)
            {
                return NotFound();
            }

            return Ok(j_errors);
        }

        // POST: api/j_errors
        [ResponseType(typeof(j_errors))]
        [HttpPost]
        public IHttpActionResult PostErrors(j_errors j_errors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.j_errors.Add(j_errors);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (j_errorsExists(j_errors.error_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [ResponseType(typeof(j_errors))]
        [HttpPut]
        public IHttpActionResult PostMultiErrors(j_errors[] j_errors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.j_errors.AddRange(j_errors);

            try
            {
                db.SaveChanges();
            }
            catch { }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool j_errorsExists(string id)
        {
            return db.j_errors.Count(e => e.error_id == id) > 0;
        }
    }
}