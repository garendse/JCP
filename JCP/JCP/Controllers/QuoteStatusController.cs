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
    public class QuoteStatusController : BaseApiController
    {
        private JobCostProEntities db = new JobCostProEntities();

        // GET: api/QuoteStatus
        public IQueryable<string> Getj_quote_status()
        {
            return db.j_quote_status.OrderBy(u => u.sort_order).Select(u => u.status);
        }

        // PUT: api/QuoteStatus
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutQuoteStatus(j_quote_status[] j_quote_status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.j_quote_status.RemoveRange(db.j_quote_status.ToList());

            var i = 0;
            foreach (var q in j_quote_status)
            {
                q.sort_order = i++;
            }

            db.j_quote_status.AddRange(j_quote_status);

            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool j_quote_statusExists(string id)
        {
            return db.j_quote_status.Count(e => e.status == id) > 0;
        }
    }
}