namespace Yousource.Api.Messages.Customers.Requests
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class CreateCustomerWebRequest : WebRequest
    {
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        [JsonProperty("email")]
        [Required]
        public string Email { get; set; }
    }
}
