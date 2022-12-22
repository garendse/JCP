using JCPBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JCPBackend.Controllers
{
    public class UserLogin
    {
        public string username { get; set; } = null!;
        public string password { get; set; } = null!;
        public string site_id { get; set; } = null!;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly JobCostProTestingContext _context;
        private readonly IConfiguration _config;

        public UserAuthController(JobCostProTestingContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        private static string ConvertDate(DateTime date)
        {
            return ((DateTimeOffset)date).ToUnixTimeSeconds().ToString();
        }

        public static DateTime UnixTimeStampToDateTime(Int64 unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        [AllowAnonymous]
        [HttpPost]
        public Object Auth(UserLogin user_login)
        {
            string site_id = null!;
            string site_name = null!;
            j_user user = null!;

            try
            {
                var db_user = _context.j_users.Where(u => u.username.ToLower() == user_login.username.ToLower() && u.password == user_login.password).FirstOrDefault();

                if(db_user != null) { 
                    user = db_user;
                }
                else
                {
                    throw new Exception("Incorrect login details!");
                }

                if (user.role != "SUPPORT")
                {
                    var db_site = user.sites.Where(u => u.id == user_login.site_id).FirstOrDefault();
                    if (db_site == null)
                    {
                        throw new Exception("User does not have access to site!");
                    }
                    site_id = db_site.id;
                    site_name = db_site.description;
                }
                else
                {
                    site_id = user_login.site_id;
                    site_name = _context.j_sites.Where(u => u.id == site_id).FirstOrDefault()?.description ?? "";
                }
            }
            catch(Exception ex)
            {
                return new { token = "", message = ex.Message};
            }

            var key = Encoding.ASCII.GetBytes
                (_config["JCP_JWT_SECRET"] ?? "BACKUP");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()),
                    new Claim("active", user.active.ToString(), ClaimValueTypes.Boolean),
                    new Claim("end_date", ConvertDate(user.end_date), ClaimValueTypes.Date),
                    new Claim("username", user.username),
                    new Claim("id", user.id),
                    new Claim("name", user.name),
                    new Claim("password_date", ConvertDate(user.password_date), ClaimValueTypes.Date),
                    new Claim("role", user.role),
                    new Claim("surname", user.surname),
                    new Claim("tel_no", user.tel_no ?? ""),
                    new Claim("site_id", site_id),
                    new Claim("site_name", site_name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = "",
                Audience = "",
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return new { token = stringToken };

            //return Results.Unauthorized
        }
    }
}