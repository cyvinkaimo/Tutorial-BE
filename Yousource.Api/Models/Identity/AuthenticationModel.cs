namespace Yousource.Api.Models.Identity
{
    using System;
    using Newtonsoft.Json;

    public class AuthenticationModel
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("expires")]
        public DateTime? Expires { get; set; }
    }
}
