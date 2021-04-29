namespace Yousource.Infrastructure.Messages.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Yousource.Infrastructure.Constants;

    public class SignInResponse : Response
    {
        public string AccessToken { get; set; }

        public DateTime? Expires { get; set; }

        public override Dictionary<string, int> StatusCodeMap => new Dictionary<string, int>
        {
            { string.Empty, (int)HttpStatusCode.OK },
            { IdentityServiceErrorCodes.InvalidCredential, (int)HttpStatusCode.BadRequest }
        };
    }
}
