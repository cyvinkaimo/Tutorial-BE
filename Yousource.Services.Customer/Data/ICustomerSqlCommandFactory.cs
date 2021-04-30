namespace Yousource.Services.Customer.Data
{
    using System.Data.SqlClient;
    using Yousource.Infrastructure.Entities.Customers;

    public interface ICustomerSqlCommandFactory
    {
        SqlCommand CreateInsertCustomerCommand(Customer customer);

        SqlCommand CreateGetCustomersCommand();
    }
}
