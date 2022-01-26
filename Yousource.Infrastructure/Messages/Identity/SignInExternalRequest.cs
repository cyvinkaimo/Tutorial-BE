namespace Yousource.Infrastructure.Messages.Identity
{
    using System.Runtime.Serialization;
    using Yousource.Infrastructure.Enums.Identity;

    [DataContract]
    public class SignInExternalRequest
    {
        [DataMember]
        public string Provider { get; set; }

        [DataMember]
        public string IdToken { get; set; }

        [DataMember]
        public Role DefaultRole { get; set; }
    }
}
