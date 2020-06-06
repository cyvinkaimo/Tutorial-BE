namespace Yousource.Api.Models.User
{
    using System;
    using Newtonsoft.Json;

    public class UserWebModel
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }
    }
}
