namespace Yousource.ExceptionHandling
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ArxOne.MrAdvice.Advice;
    using Newtonsoft.Json;
    using Yousource.Infrastructure.Exceptions;
    using Yousource.Infrastructure.Logging;

    public class HandleServiceException : Attribute, IMethodAsyncAdvice
    {
        private readonly Type rethrowException;

        public HandleServiceException(Type rethrowException)
        {
            this.rethrowException = rethrowException;
        }

        public async Task Advise(MethodAsyncAdviceContext context)
        {
            var logger = (context.Target as dynamic).Logger as ILogger;

            try
            {
                await context.ProceedAsync();
            }
            catch (Exception e)
            {
                var ex = (Exception)Activator.CreateInstance(this.rethrowException, e, e.Message);
                logger.WriteException(ex, new Dictionary<string, string> { { "args", JsonConvert.SerializeObject(context.Arguments) } });
                throw ex;
            }
        }
    }
}