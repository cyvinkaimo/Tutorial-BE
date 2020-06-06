namespace Yousource.Infrastructure.Models.Customers
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Customer
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public DateTime? UpdatedAt { get; set; }
    }
}
