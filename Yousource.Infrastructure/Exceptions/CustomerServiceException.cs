namespace Yousource.Infrastructure.Exceptions
{
    using System;

    public class CustomerServiceException : RethrowableException
    {
        public CustomerServiceException(Exception innerException, string message)
            : base(innerException, message)
        {
        }

        public CustomerServiceException(Exception innerException)
            : base(innerException, string.Empty)
        {
        }
    }
}
