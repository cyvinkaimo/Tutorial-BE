namespace Yousource.Api.Messages.Identity
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class SignInWebRequest
    {
        [Required]
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
