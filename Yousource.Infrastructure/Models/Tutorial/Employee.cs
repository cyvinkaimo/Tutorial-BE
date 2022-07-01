namespace Yousource.Infrastructure.Models.Tutorial
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Employee
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public string Address { get; set; }
    }
}
