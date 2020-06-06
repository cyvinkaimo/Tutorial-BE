namespace Yousource.Infrastructure.Messages.Customers.Responses
{
    using System.Collections.Generic;
    using System.Net;
    using System.Runtime.Serialization;
    using Yousource.Infrastructure.Messages;

    [DataContract]
    public class CreateCustomerResponse : Response
    {
        public override Dictionary<string, int> StatusCodeMap => new Dictionary<string, int>
        {
            { string.Empty, (int)HttpStatusCode.Created },
            { "customer/create-validation-error", (int)HttpStatusCode.UnprocessableEntity }
        };
    }
}
