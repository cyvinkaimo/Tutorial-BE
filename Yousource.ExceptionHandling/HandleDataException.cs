namespace Yousource.ExceptionHandling
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Threading.Tasks;
    using ArxOne.MrAdvice.Advice;
    using Newtonsoft.Json;
    using Yousource.Infrastructure.Exceptions;
    using Yousource.Infrastructure.Logging;

    public class HandleDataException : Attribute, IMethodAsyncAdvice
    {
        private readonly Type rethrowException;

        public HandleDataException(Type rethrowException = null)
        {
            this.rethrowException = rethrowException ?? typeof(DataException);
        }

        public async Task Advise(MethodAsyncAdviceContext context)
        {
            var logger = (context.Target as dynamic).Logger as ILogger;

            try
            {
                await context.ProceedAsync();
            }
            catch (DbException e)
            {
                var ex = (DbException)Activator.CreateInstance(this.rethrowException, e, e.Message);
                logger.WriteException(ex, new Dictionary<string, string> { { "args", JsonConvert.SerializeObject(context.Arguments) } });
                throw ex;
            }
        }
    }
}
