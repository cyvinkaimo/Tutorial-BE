namespace Yousource.Services.Customer.Data
{
    using System.Data;
    using System.Data.SqlClient;
    using Yousource.Infrastructure.Entities.Customers;

    /// <summary>
    /// The SqlCommandFactory helpers separate creation of SqlCommand from the Data Gateway for a shorter, one-line look.
    /// This is an MS SQL-only specific helper class and will not apply to other technologies like Cosmos, Mongo, Postgres and etc
    /// </summary>
    public static class CustomerSqlCommandFactory
    {
        public static SqlCommand CreateGetCustomersCommand()
        {
            var result = new SqlCommand("[dbo].[sp_GetCustomers]")
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60
            };

            return result;
        }

        public static SqlCommand CreateInsertCustomerCommand(Customer customer)
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
