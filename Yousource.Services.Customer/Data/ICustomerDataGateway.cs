namespace Yousource.Services.Customer.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Yousource.Infrastructure.Entities.Customers;

    public interface ICustomerDataGateway
    {
        Task InsertCustomerAsync(Customer customer);

        Task<IEnumerable<Customer>> GetCustomersAsync();
    }
}
