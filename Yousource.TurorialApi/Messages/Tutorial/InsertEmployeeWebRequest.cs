namespace Yousource.TutorialApi.Messages.Identity
{
    using Newtonsoft.Json;

    public class InsertEmployeeWebRequest
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
