namespace Yousource.Api.Messages.Identity
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class SignUpWebRequest
    {
        [JsonProperty("userName")]
        [Required]
        public string UserName { get; set; }

        [JsonProperty("password")]
        [Required]
        public string Password { get; set; }

        [JsonProperty("confirmPassword")]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
