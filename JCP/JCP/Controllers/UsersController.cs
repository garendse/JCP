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
    public class UsersController : BaseApiController
    {
        private JobCostProEntities db = new JobCostProEntities();

        // GET: api/Users
        public IQueryable<j_users> Getj_users()
        {
            var data = db.j_users;
            foreach(var d in data)
            {
                d.password = "";
            }

            return data;
        }

        // GET: api/Users/5
        [ResponseType(typeof(j_users))]
        public IHttpActionResult Getj_users(string id)
        {
            j_users j_users = db.j_users.Find(id);
            if (j_users == null)
            {
                return NotFound();
            }

            j_users.password = "";

            return Ok(j_users);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putj_users(string id, j_users j_users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != j_users.id)
            {
                return BadRequest();
            }

            db.Entry(j_users).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!j_usersExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(j_users))]
        public IHttpActionResult Postj_users(j_users j_users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            j_users.id = Guid.NewGuid().ToString();

            db.j_users.Add(j_users);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (j_usersExists(j_users.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = j_users.id }, j_users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool j_usersExists(string id)
        {
            return db.j_users.Count(e => e.id == id) > 0;
        }
    }
}