namespace Yousource.Infrastructure.Messages.Identity
{
    using System.Collections.Generic;
    using System.Net;
    using Yousource.Infrastructure.Constants;

    public class AddToRoleResponse : Response
    {
        public override Dictionary<string, int> StatusCodeMap => new Dictionary<string, int>
        {
            { string.Empty, (int)HttpStatusCode.OK },
            { IdentityServiceErrorCodes.UserNotFound, (int)HttpStatusCode.BadRequest }
        };
    }
}
