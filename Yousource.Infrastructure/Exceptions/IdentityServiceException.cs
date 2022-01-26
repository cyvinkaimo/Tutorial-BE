namespace Yousource.Infrastructure.Exceptions
{
    using System;

    public class IdentityServiceException : Exception
    {
        public IdentityServiceException(Exception ex)
            : base(ex.Message, ex)
        {
        }
    }
}
