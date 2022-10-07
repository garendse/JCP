using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace JCP.Filters
{
    public class JWTAuth : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                return;
            }

            if (authorization.Scheme != "Bearer")
            {
                return;
            }

            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
                return;
            }

            string JWT = authorization.Parameter;
            if (JWT == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", request);
            }

            IPrincipal principal = await AuthenticateJwtToken(JWT);
            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);
            }
            else
            {
                context.Principal = principal;
            }
        }


        private static bool ValidateToken(string token, out j_users user,  out string site_id, out string site_name)
        {
            user = new j_users();
            site_id = "";
            site_name = "";

            var simplePrinciple = JWTManager.GetPrincipal(token);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var claims = identity.Claims.ToList();

            user.username = claims.Where(c => c.Type == "username").First().Value;
            user.id = identity.Claims.Where(c => c.Type == "id").Select(c => c.Value).SingleOrDefault();
            user.name = identity.Claims.Where(c => c.Type == "name").Select(c => c.Value).SingleOrDefault();
            user.password_date = JWTManager.UnixTimeStampToDateTime(Int64.Parse(identity.Claims.Where(c => c.Type == "password_date").Select(c => c.Value).SingleOrDefault()));
            user.role = identity.Claims.Where(c => c.Type == "role").Select(c => c.Value).SingleOrDefault();
            user.surname = identity.Claims.Where(c => c.Type == "surname").Select(c => c.Value).SingleOrDefault();
            user.tel_no = identity.Claims.Where(c => c.Type == "tel_no").Select(c => c.Value).SingleOrDefault();
            user.active = Boolean.Parse(identity.Claims.Where(c => c.Type == "active").Select(c => c.Value).SingleOrDefault());
            user.end_date = JWTManager.UnixTimeStampToDateTime(Int64.Parse(identity.Claims.Where(c => c.Type == "end_date").Select(c => c.Value).SingleOrDefault()));

            site_id = identity.Claims.Where(c => c.Type == "site_id").Select(c => c.Value).SingleOrDefault();
            site_name = identity.Claims.Where(c => c.Type == "site_name").Select(c => c.Value).SingleOrDefault();

            if (string.IsNullOrEmpty(user.id))
                return false;

            return true;
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            if (ValidateToken(token, out var user, out var site_id, out var site_name))
            {
                try
                {
                    var claims = new List<Claim>
                        {
                        new Claim("active", user.active.ToString(), ClaimValueTypes.Boolean),
                        new Claim("end_date", user.end_date.ToUniversalTime().ToString(), ClaimValueTypes.Date),
                        new Claim("username", user.name),
                        new Claim("name", user.name),
                        new Claim("password_date", user.password_date.ToUniversalTime().ToString(), ClaimValueTypes.Date),
                        new Claim("role", user.role ?? ""),
                        new Claim("surname", user.surname),
                        new Claim("tel_no", user.tel_no),
                        new Claim("site_id", site_id),
                        new Claim("site_name", site_name),
                        new Claim("id", user.id)
                    };

                    var identity = new ClaimsIdentity(claims, "Jwt");
                    IPrincipal userPrincipal = new ClaimsPrincipal(identity);

                    return Task.FromResult(userPrincipal);
                
                }
                catch(Exception e)
                {

                }
              
            }

            return Task.FromResult<IPrincipal>(null);
        }


        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = null;

            if (!string.IsNullOrEmpty(Realm))
                parameter = "realm=\"" + Realm + "\"";

            context.ChallengeWith("Bearer", parameter);
        }
    }
}