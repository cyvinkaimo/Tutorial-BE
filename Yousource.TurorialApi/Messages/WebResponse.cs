namespace Yousource.TutorialApi.Messages
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using Newtonsoft.Json;
    using Yousource.TutorialApi.Constants;

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WebResponse<T> : WebResponse
    {
        public WebResponse(T data)
        {
            this.Data = data;
        }

        public WebResponse()
        {
        }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WebResponse
    {
        public WebResponse()
        {
            this.Message = string.Empty;
        }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty("errorCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorCode { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Dictionary<string, HttpStatusCode> StatusCodeMap => new Dictionary<string, HttpStatusCode>
        {
            { string.Empty, HttpStatusCode.OK },
            { Errors.ModelState, HttpStatusCode.UnprocessableEntity },
        };
    }
}
