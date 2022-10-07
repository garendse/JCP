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
    public class VehiclesController : BaseApiController
    {
        private JobCostProEntities db = new JobCostProEntities();

        // GET: api/Vehicles
        [JWTAuth]
        public IQueryable<Object> GetVehicles(string customer_id = "", string reg = "")
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            return db
                .j_vehicles
                .Where(u =>
                u.site_access_id == site &&
                (u.customer_id.Contains(customer_id) && u.registration.Contains(reg)));
        }

        // GET: api/Vehicles/5
        [JWTAuth]
        [ResponseType(typeof(j_vehicles))]
        public IHttpActionResult GetVehicles(string id)
        {
            j_vehicles j_vehicles = db.j_vehicles.Find(id);
            if (j_vehicles == null)
            {
                return NotFound();
            }

            return Ok(j_vehicles);
        }

        // PUT: api/Vehicles/5
        [ResponseType(typeof(void))]
        [JWTAuth]
        public IHttpActionResult PutVehicles(string id, j_vehicles j_vehicles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != j_vehicles.id)
            {
                return BadRequest();
            }

            db.Entry(j_vehicles).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!j_vehiclesExists(id))
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

        // POST: api/Vehicles
        [JWTAuth]
        [ResponseType(typeof(j_vehicles))]
        public IHttpActionResult PostVehicles(j_vehicles vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            vehicle.site_access_id = site;         
            vehicle.id = Guid.NewGuid().ToString();

            if (db.j_vehicle_models.Count(u => (u.brand == vehicle.brand && u.model == vehicle.model)) == 0)
            {
                db.j_vehicle_models.Add(new j_vehicle_models { brand = vehicle.brand, model = vehicle.model });
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {

                }
            }
            db.j_vehicles.Add(vehicle);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (j_vehiclesExists(vehicle.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vehicle.id }, vehicle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool j_vehiclesExists(string id)
        {
            return db.j_vehicles.Count(e => e.id == id) > 0;
        }
    }
}