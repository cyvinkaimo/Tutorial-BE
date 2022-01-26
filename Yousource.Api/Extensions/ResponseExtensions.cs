namespace Yousource.Api.Extensions
{
    using Yousource.Api.Messages;
    using Yousource.Infrastructure.Messages;

    public static class ResponseExtensions
    {
        public static WebResponse AsWebResponse(this Response response)
        {
            var result = new WebResponse
            {
                ErrorCode = response.ErrorCode,
                Message = response.Message,
            };
            return result;
        }
    }
}
