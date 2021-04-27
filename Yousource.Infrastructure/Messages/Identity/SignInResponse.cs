namespace Yousource.Infrastructure.Messages.Identity
{
    using System;

    public class SignInResponse : Response
    {
        public string AccessToken { get; set; }

        public DateTime? Expires { get; set; }
    }
}
