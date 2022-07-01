namespace Yousource.Infrastructure.Entities.Tutorial
{
    using System;
    using Newtonsoft.Json;

    public class EmployeeEntity
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
