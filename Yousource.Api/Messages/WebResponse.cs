namespace Yousource.Api.Messages
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Text.Json.Serialization;

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WebResponse<T> : WebResponse
    {
        public WebResponse(T data)
        {
            this.Data = data;
        }

        [JsonPropertyName("data")]
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

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("errorCode")]
        public string ErrorCode { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}
