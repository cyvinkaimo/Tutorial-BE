namespace Yousource.Services.Identity.Extensions
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Yousource.Infrastructure.Entities.Identity;
    using Yousource.Services.Identity.Helpers;

    internal static class UserManagerExtensions
    {
        public static async Task<string> GenerateJwtAsync(this UserManager<User> userManager, JwtHelper jwtHelper, User user)
        {
            var claims = await userManager.GetClaimsAsync(user);
            var result = jwtHelper.GenerateToken(claims);
            return result;
        }
    }
}
