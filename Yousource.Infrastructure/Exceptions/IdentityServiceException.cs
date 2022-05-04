﻿namespace Yousource.Infrastructure.Exceptions
{
    using System;

    public class IdentityServiceException : RethrowableException
    {
        public IdentityServiceException(Exception ex)
            : base(ex, ex.Message)
        {
        }
    }
}
