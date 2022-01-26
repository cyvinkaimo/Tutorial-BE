﻿namespace Yousource.Api.Messages.Identity
{
    using System.Collections.Generic;
    using System.Net;
    using Yousource.Api.Messages;
    using Yousource.Infrastructure.Constants.Errors;

    public class SignInWebResponse : WebResponse<string>
    {
        public override Dictionary<string, HttpStatusCode> StatusCodeMap => new Dictionary<string, HttpStatusCode>
        {
            { string.Empty, HttpStatusCode.OK },
            { IdentityServiceErrorCodes.InvalidCredential, HttpStatusCode.BadRequest }
        };
    }
}
