using Dictant.Server.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace Dictant.Server.Helpers
{
    public class UserHelper
    {
        public static string GetName(HttpContext context)
        {
            var token = ((string)context.Request.Headers.Values.Skip(4).First()).Replace("bearer ", "");
            var key = Encoding.ASCII.GetBytes(((JwtSecretKeyService)context.RequestServices.GetService(typeof(JwtSecretKeyService))).SecretKey);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            return claims.Identity.Name;
        }
    }

}
