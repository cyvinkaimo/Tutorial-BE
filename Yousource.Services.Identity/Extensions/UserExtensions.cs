namespace Yousource.Services.Identity.Extensions
{
    using Yousource.Infrastructure.Entities.Identity;
    using Yousource.Infrastructure.Messages.Identity;

    public static class UserExtensions
    {
        public static User AsUser(this SignUpRequest request)
        {
            var result = new User
            {
                UserName = request.UserName,
                Email = request.UserName
            };

            return result;
        }
    }
}
