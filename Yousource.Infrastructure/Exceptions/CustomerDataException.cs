namespace Yousource.Infrastructure.Exceptions
{
    using System;

    public class CustomerDataException : RethrowableException
    {
        public CustomerDataException(Exception innerException)
            : base(innerException, string.Empty)
        {
        }
    }
}
