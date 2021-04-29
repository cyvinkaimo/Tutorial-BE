namespace Yousource.Api.Messages
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using Newtonsoft.Json;

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WebResponse<T> : WebResponse
    {
        public WebResponse(T data)
        {
            this.Data = data;
        }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WebResponse
    {
        public WebResponse()
        {
            this.StatusCode = (int)HttpStatusCode.OK;
            this.Message = string.Empty;
        }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty("errorCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorCode { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}
