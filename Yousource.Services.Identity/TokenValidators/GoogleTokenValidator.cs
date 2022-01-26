namespace Yousource.Services.Identity.TokenValidators
{
    using System.Collections.Generic;
    using Google.Apis.Auth;
    using Yousource.Services.Identity.Models;

    internal class GoogleTokenValidator : ITokenValidator
    {
        private readonly string clientId;

        public GoogleTokenValidator(string clientId)
        {
            this.clientId = clientId;
        }

        public TokenPayload ValidateToken(string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { clientId }
            };

            var payload = GoogleJsonWebSignature.ValidateAsync(idToken, settings).Result;

            var result = new TokenPayload { Subject = payload.Subject, Email = payload.Email, GivenName = payload.GivenName, FamilyName = payload.FamilyName };
            return result;
        }
    }
}
