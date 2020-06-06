namespace Yousource.Services.Customer.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Yousource.Services.Customer.Data.Entities;

    public interface ICustomerDataGateway
    {
        Task InsertCustomerAsync(CustomerEntity customer);

        Task<IEnumerable<CustomerEntity>> GetCustomersAsync();
    }
}
