using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;


namespace JCP
{
    public class JWTManager
    {
        private const string secret = "h6k3OqBna+zfpqti0N58XMo/7/WuXdoCgwkq0v193+HJHnj2mheqF1Uy8sTzfIXE7y+E0fnQx4gZmXWGRv0aXLryx8PbUCggbTIVqG2GLFINT0qyuLnFFGw1grRD4GYjzsQP8MTDYSblUkh5m9Z/av60dHguxJn4EP56RdXR4FYskSf8qajWNvRxMx1gp849Bf8xJ3tfiY87h2ZAeL2fDO6rHK6pmhyod/kCZ4kAwImuD83ap2Vv/QATgBW8xzGP1ZFNnV2IMge50kXdli1oi1kTqfVknXRKxbx6SfpKa3u+InnBYHlZ4g53s4mpZrY6zG6iVnEIC0FyEbTZTgRH";

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

        public static string GenerateToken(j_users user, string site_id, string site_name, int validHours = 10)
        {
            var symmetricKey = Convert.FromBase64String(secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
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

                Expires = now.AddHours(Convert.ToInt32(validHours)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                return principal;
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}