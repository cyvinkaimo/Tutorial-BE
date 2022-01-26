namespace Yousource.Services.Identity.Extensions
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Yousource.Services.Identity.Models;

    internal static class TokenPayloadExtensions
    {
        public static IEnumerable<Claim> AsClaims(this TokenPayload payload, string userId = "")
        {
            var result = new List<Claim>
            {
                new Claim(ClaimTypes.Email, payload.Email),
                new Claim(ClaimTypes.Name, payload.Email)
            };

            if (!string.IsNullOrEmpty(payload.GivenName))
            {
                result.Add(new Claim(ClaimTypes.GivenName, payload.GivenName));
            }

            if (!string.IsNullOrEmpty(payload.FamilyName))
            {
                result.Add(new Claim(ClaimTypes.Surname, payload.FamilyName));
            }

            if (!string.IsNullOrEmpty(userId))
            {
                result.Add(new Claim(ClaimTypes.NameIdentifier, userId));
            }

            return result;
        }
    }
}
