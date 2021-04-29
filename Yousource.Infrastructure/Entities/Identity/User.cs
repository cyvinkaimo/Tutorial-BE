namespace Yousource.Infrastructure.Entities.Identity
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser<Guid>
    {
        //// All custom user fields should go here.

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
