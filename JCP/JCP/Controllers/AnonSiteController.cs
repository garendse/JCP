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
    public class AnonSiteController : BaseApiController
    {
        private JobCostProEntities db = new JobCostProEntities();

        // GET: api/AnonSite
        public IQueryable<j_sites> Getj_sites()
        {
            return db.j_sites;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool j_sitesExists(string id)
        {
            return db.j_sites.Count(e => e.id == id) > 0;
        }
    }
}