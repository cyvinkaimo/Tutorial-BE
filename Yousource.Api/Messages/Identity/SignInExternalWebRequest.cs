namespace Yousource.Api.Messages.Identity
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class SignInExternalWebRequest
    {
        [Required]
        [JsonProperty("idToken")]
        public string IdToken { get; set; }

        [Required]
        [JsonProperty("provider")]
        public string Provider { get; set; }
    }
}
