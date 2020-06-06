namespace Yousource.Infrastructure.Logging
{
    using System;

    public interface ILogger
    {
        void WriteException(Exception e);
    }
}
