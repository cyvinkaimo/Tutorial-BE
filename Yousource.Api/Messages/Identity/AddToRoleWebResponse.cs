namespace Yousource.Api.Messages.Identity
{
    using System.Collections.Generic;
    using System.Net;
    using Yousource.Infrastructure.Constants.Errors;

    public class AddToRoleWebResponse : Messages.WebResponse
    {
        public override Dictionary<string, HttpStatusCode> StatusCodeMap => new Dictionary<string, HttpStatusCode>
        {
            { string.Empty, HttpStatusCode.OK },
            { IdentityServiceErrorCodes.UserNotFound, HttpStatusCode.BadRequest }
        };
    }
}
