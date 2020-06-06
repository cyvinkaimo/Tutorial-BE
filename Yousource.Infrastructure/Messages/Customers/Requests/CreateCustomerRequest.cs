namespace Yousource.Infrastructure.Messages.Customers.Requests
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CreateCustomerRequest
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}
