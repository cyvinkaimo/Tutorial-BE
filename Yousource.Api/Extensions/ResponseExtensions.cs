namespace Yousource.Api.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Yousource.Infrastructure.Messages;
    using Yousource.Api.Messages;

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
