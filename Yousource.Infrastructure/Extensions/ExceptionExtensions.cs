namespace Yousource.Infrastructure.Extensions
{
    using System;

    public static class ExceptionExtensions
    {
        public static Exception GetInnermostException(this Exception exception)
        {
            var result = exception;

            if (result != null)
            {
                while (result.InnerException != null)
                {
                    result = result.InnerException;
                }
            }

            return result;
        }
    }
}
