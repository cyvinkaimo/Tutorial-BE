namespace Yousource.Services.Customer.Exceptions
{
    using System;

    public class CustomerServiceException : Exception
    {
        public CustomerServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public CustomerServiceException(Exception innerException)
            : base(string.Empty, innerException)
        {
        }
    }
}
