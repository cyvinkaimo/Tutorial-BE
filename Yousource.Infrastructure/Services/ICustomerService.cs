namespace Yousource.Infrastructure.Services
{
    using System.Threading.Tasks;
    using Yousource.Infrastructure.Messages.Customers.Requests;
    using Yousource.Infrastructure.Messages.Customers.Responses;

    public interface ICustomerService
    {
        Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest request);

        Task<GetCustomersResponse> GetCustomersAsync();
    }
}
