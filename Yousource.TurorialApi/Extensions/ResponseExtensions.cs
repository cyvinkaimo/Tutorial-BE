namespace Yousource.TutorialApi.Extensions
{
    using Yousource.TutorialApi.Messages;
    using Yousource.Infrastructure.Messages;

    public static class ResponseExtensions
    {
        public static WebResponse AsWebResponse(this Response response)
        {
            var result = new WebResponse
            {
                ErrorCode = response.ErrorCode,
                Message = response.Message
            };
            return result;
        }

        public static WebResponse AsWebResponse<T>(this Response<T> response)
        {
            var result = new WebResponse<T>
            {
                Data = response.Data,
                ErrorCode = response.ErrorCode,
                Message = response.Message
            };
            return result;
        }
    }
}
