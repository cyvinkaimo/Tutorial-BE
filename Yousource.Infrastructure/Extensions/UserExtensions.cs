namespace Yousource.Infrastructure.Extensions
{
    using System.Collections.Generic;
    using Yousource.Infrastructure.Entities.Identity;

    public static class UserExtensions
    {
        public static IDictionary<string, string> CreateLogProperties(this User user)
        {
            var result = new Dictionary<string, string>
            {
                { "userId", user.Id.ToString() },
                { "email", user.Email }
            };

            return result;
        }
    }
}
