namespace Yousource.Services.Identity.TokenValidators
{
    using Yousource.Services.Identity.Models;

    internal interface ITokenValidator
    {
        TokenPayload ValidateToken(string idToken);
    }
}
