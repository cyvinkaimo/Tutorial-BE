namespace Yousource.Infrastructure.Messages.Customers.Responses
{
    using System.Collections.Generic;
    using System.Net;
    using System.Runtime.Serialization;
    using Yousource.Infrastructure.Messages;
    using Yousource.Infrastructure.Models.Customers;

    public class GetCustomersResponse : Response
    {
        [DataMember]
        public IEnumerable<Customer> Data { get; set; }
    }
}
