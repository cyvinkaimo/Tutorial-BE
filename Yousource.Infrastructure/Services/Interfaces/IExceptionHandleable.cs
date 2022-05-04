namespace Yousource.Infrastructure.Services.Interfaces
{
    using Yousource.Infrastructure.Logging;

    public interface IExceptionHandleable
    {
        ILogger Logger { get; }
    }
}
