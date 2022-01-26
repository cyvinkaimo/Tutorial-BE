namespace Yousource.Infrastructure.Workflows
{
    using System.Threading.Tasks;

    public abstract class AsyncStep<TRequest, TResponse>
    {
        private AsyncStep<TRequest, TResponse> next;

        public abstract Task<TResponse> ExecuteAsync(TRequest request);

        public virtual Task<TResponse> ExecuteNextAsync(TRequest request)
        {
            if (this.next != null)
            {
                return this.next.ExecuteAsync(request);
            }

            return null;
        }

        public void SetNextStep(AsyncStep<TRequest, TResponse> step)
        {
            if (this.next != null)
            {
                this.next.SetNextStep(step);
            }
            else
            {
                this.next = step;
            }
        }
    }
}
