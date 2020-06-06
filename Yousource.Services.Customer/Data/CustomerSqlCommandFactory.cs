namespace Yousource.Services.Customer.Data
{
    using System.Data;
    using System.Data.SqlClient;
    using Yousource.Services.Customer.Data.Entities;

    public class CustomerSqlCommandFactory : ICustomerSqlCommandFactory
    {
        public SqlCommand CreateGetCustomersCommand()
        {
            var result = new SqlCommand("[dbo].[sp_GetCustomers]")
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60
            };

            return result;
        }

        public SqlCommand CreateInsertCustomerCommand(CustomerEntity customer)
        {
            var result = new SqlCommand("[dbo].[sp_InsertCustomer]")
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60
            };

            result.Parameters.AddWithValue("@name", customer.Name);
            result.Parameters.AddWithValue("@email", customer.Email);
            //// Add all fields as parameters

            return result;
        }
    }
}
