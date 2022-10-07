using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using JCP.Filters;
using System.Security.Claims;

namespace JCP.Controllers
{
    public class QuotesController : BaseApiController
    {
        private JobCostProEntities db = new JobCostProEntities();

        // GET: api/Quotes
        [JWTAuth]
        public IQueryable<Object> Getj_quotes(string branch_id = "")
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            // Lazy loading shoud be off because we are serialising the result.
            db.Configuration.LazyLoadingEnabled = false;
            return db.j_quotes.Where(u => u.site_access == site).Include("vehicle").Include("customer").Include("tech");
        }

        // GET: api/Quotes/5
        [JWTAuth]
        [ResponseType(typeof(j_quotes))]
        public IHttpActionResult Getj_quotes(string id, string branch_id = "")
        {
            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            // Lazy loading shoud be off because we are serialising the result.
            db.Configuration.LazyLoadingEnabled = false;

            var data = 
                db.j_quotes
                  .Where(u => u.id == id && u.site_access == site)
                  .Include("vehicle")
                  .Include("customer")
                  .Include("items")
                  .Include("tech")
                  .Include("create_user")
                  .Include("update_user")
                  .Include("items.subquotes.user")
                  .Include("items.subquotes.supplier")
                  .Include("items.subquotes.supplier.supplier").FirstOrDefault();

            // Why cant .NET do this automatically? Investigate.
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        // PUT: api/Quotes/5
        [JWTAuth]
        [ResponseType(typeof(j_quotes))]
        public IHttpActionResult Putj_quotes(string id, j_quotes quote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var claims = (User.Identity as ClaimsIdentity).Claims;
            var userid = claims.Where(u => u.Type == "id").First().Value;

            //Can fail if id != quote_id, check it before commiting to the DB
            if (id != quote.id)
            {
                return BadRequest();
            }

            // EF is not smart enough to figure out whether to attach or add. Do it manualy
            foreach (var item in quote.items)
            {
                if (item.job_code == "")
                    continue;

                foreach (var sq in item.subquotes)
                {
                    sq.supplier = db.j_supplier_branches.Where(u => u.id == sq.supplier_id).Include("supplier").FirstOrDefault();

                    sq.user = db.j_users.Where(u => u.id == sq.accepted_by_user_id).FirstOrDefault();

                    if (sq.id == null)
                    {
                        // Gen a UUID because SQL Server does not support generating UUIDs.
                        sq.id = Guid.NewGuid().ToString();

                        // If the ID does not exist the subquote does not either. Add to the db, but first check for FK violations
                        if(!(item.id == null || item.id == ""))
                            db.j_quote_item_supplier.Add(sq);
                    }
                    else
                    {
                        db.Entry(sq).State = EntityState.Modified;
                    }
                    db.SaveChanges();

                }
                // Gen a UUID because SQL Server does not support generating UUIDs as defualt values.
                if (item.id == null || item.id == "")
                {
                    item.id = Guid.NewGuid().ToString();
                    db.j_quote_items.Add(item);
                }
                else
                {
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            quote.create_user = db.j_users.Where(u => u.id == quote.create_user_id).FirstOrDefault();
            quote.update_user_id = userid;
            quote.update_user = db.j_users.Where(u => u.id == userid).FirstOrDefault();
            quote.update_datetime = DateTime.Now;
            quote.tech = db.j_techs.Where(u => u.id == quote.tech_id).FirstOrDefault();

            // Finally update the quote in the DB after all deps have been updated
            db.Entry(quote).State = EntityState.Modified;

            db.SaveChanges();

            return Ok(quote);
        }

        // POST: api/Quotes
        [JWTAuth]
        [ResponseType(typeof(j_quotes))]
        public IHttpActionResult Postj_quotes(j_quotes quote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rnd = new Random(DateTime.Now.Millisecond);

            if (quote.ro_number == "" || db.j_quotes.Where(u => u.ro_number == quote.ro_number).Count() > 0)
            {
                do
                {
                    quote.ro_number = rnd.Next(10000, 100_000).ToString();
                }
                while (db.j_quotes.Where(u => u.ro_number == quote.ro_number).Count() > 0);
            }

            // Get the user site
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var site = claims.Where(u => u.Type == "site_id").First().Value;

            // Generate a UUID because SQL server does not do it automatically
            quote.id = Guid.NewGuid().ToString();
            quote.site_access = site;

            // Everything _should_ be ok to add here without linking related data first.
            db.j_quotes.Add(quote);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return InternalServerError();
            }

            var n_quote = Getj_quotes(quote.id, "");

            return n_quote;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool j_quotesExists(string id)
        {
            return db.j_quotes.Count(e => e.id == id) > 0;
        }
    }
}