namespace Yousource.Infrastructure.Messages.Identity
{
    using System.Collections.Generic;
    using System.Net;

    public class SignUpResponse : Response
    {
        public override Dictionary<string, int> StatusCodeMap => new Dictionary<string, int>
        {
            { string.Empty, (int)HttpStatusCode.Created }
        };
    }
}
