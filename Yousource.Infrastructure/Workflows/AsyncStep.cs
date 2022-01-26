namespace Yousource.Infrastructure.Workflows
{
    using System.Threading.Tasks;

    public abstract class AsyncStep<TRequest, TResponse>
    {
        private AsyncStep<TRequest, TResponse> next;

        public abstract Task<TResponse> ExecuteAsync(TRequest request);

        public virtual Task<TResponse> ExecuteNextAsync(TRequest request)
        {
            if (next != null)
            {
                return next.ExecuteAsync(request);
            }

            return null;
        }

        public void SetNextStep(AsyncStep<TRequest, TResponse> step)
        {
            if (next != null)
            {
                next.SetNextStep(step);
            }
            else
            {
                next = step;
            }
        }
    }
}
