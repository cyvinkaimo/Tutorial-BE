namespace Yousource.Services.Customer.Data
{
    using System.Data.SqlClient;
    using Yousource.Services.Customer.Data.Entities;

    public interface ICustomerSqlCommandFactory
    {
        SqlCommand CreateInsertCustomerCommand(CustomerEntity customer);

        SqlCommand CreateGetCustomersCommand();
    }
}
