using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using JCP.Filters;

namespace JCP.Controllers
{
    public class UserController : BaseApiController
    {
        private JobCostProEntities db = new JobCostProEntities();

        // GET: api/user
        [JWTAuth]
        public IQueryable<object> Get()
        {
            return db.j_users.Select(u => new { u.username, u.name, u.surname, u.role, u.tel_no });
        }

        // GET: api/user/{id}
        [JWTAuth]
        [ResponseType(typeof(j_users))]
        public IHttpActionResult Get(string id)
        {
            j_users j_users = db.j_users.Find(id);
            if (j_users == null)
            {
                return NotFound();
            }

            // Don't leak password
            j_users.password = "";

            return Ok(j_users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}