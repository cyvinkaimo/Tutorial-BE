namespace Yousource.Services.Identity.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using Yousource.Infrastructure.Settings;
    using Yousource.Services.Identity.Models;
    using Yousource.Services.Identity.TokenValidators;

    public class JwtHelper
    {
        private readonly string clientId;
        private readonly JwtSettings settings;

        public JwtHelper()
        {
        }

        public JwtHelper(string clientId, JwtSettings settings)
        {
            this.clientId = clientId;
            this.settings = settings;
        }

        public virtual TokenPayload ValidateToken(string idToken, string provider)
        {
            // Create strategy context (use provider to toggle)
            var validator = new GoogleTokenValidator(this.clientId);
            return validator.ValidateToken(idToken);
        }

        public virtual string GenerateToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(this.settings.ExpiresInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);
            return result;
        }
    }
}
