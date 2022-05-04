namespace Yousource.Infrastructure.Exceptions
{
    using System;

    public class DataException : RethrowableException
    {
        public DataException(Exception ex, string message = "")
                  : base(ex, message ?? ex.Message)
        {
        }
    }
}
