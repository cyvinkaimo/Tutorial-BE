namespace Yousource.Services.Identity.Exceptions
{
    using System;

    public class IdentityException : Exception
    {
        public IdentityException(Exception ex)
            : base(ex.Message, ex)
        {
        }
    }
}
