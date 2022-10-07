using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace JCP.Controllers
{
    public class UserLogin
    {
        public string username { get; set; }
        public string password { get; set; }
        public string site_id { get; set; }
    }


    public class UserAuthController : BaseApiController
    {
        private JobCostProEntities db = new JobCostProEntities();

        [AllowAnonymous]
        public Object Post([FromBody]UserLogin user_login)
        {
            try
            {
                var user = db.j_users.Where(u => (u.username == user_login.username && u.password == user_login.password)).First();

                if (user == null)
                    throw new Exception("User does not exist");

                if (!user.active)
                    throw new Exception("User is not active");

                j_sites site = null;

                if (user.role != "SUPPORT")
                    site = db.j_sites.Include(s => s.users.Where(u => u.username == user_login.username)).Where(u => (u.id == user_login.site_id)).FirstOrDefault();
                else 
                    site = db.j_sites.Where(u => (u.id == user_login.site_id)).FirstOrDefault();

                if(site == null)
                    throw new Exception("User does not have access to this site!");

                return Ok(new { token = JWTManager.GenerateToken(user, site.id, site.description) });
            }
            catch
            {
                return Ok(new { token = "", message = "Invalid username or password or user expired" });
            }
        }
    }
}
