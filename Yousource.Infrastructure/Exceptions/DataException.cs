namespace Yousource.Infrastructure.Exceptions
{
    using System;

    public class DataException : Exception
    {
        public DataException(Exception ex, string message = "")
                  : base(message ?? ex.Message, ex)
        {
        }
    }
}
