using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictant.Server.Helpers
{
    public class UserHelper
    {
        public static string GetName(HttpContext context)
        {
            var token = ((string)context.Request.Headers.Values.Skip(4).First()).Replace("bearer ", "");
            string secret = "LKM3LKM344NKSJDFN4KJ345N43KJN4KJFNKDJFSNDKFJN4KJKJN4";
            var key = Encoding.ASCII.GetBytes(secret);
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
