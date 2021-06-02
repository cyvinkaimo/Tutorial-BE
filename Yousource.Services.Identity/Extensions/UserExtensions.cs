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
                //// The UserName can also be an Email. If that's the case, then also assign the Email Field below
                UserName = request.UserName,
                //// Email = request.UserName
                //// Map Other Fields
            };

            return result;
        }
    }
}
