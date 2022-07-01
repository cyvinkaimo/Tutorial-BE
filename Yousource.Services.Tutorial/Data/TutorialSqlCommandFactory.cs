namespace Yousource.Services.Tutorial.Data
{
    using System.Data;
    using System.Data.SqlClient;
    using Yousource.Infrastructure.Entities.Customers;
    using Yousource.Infrastructure.Entities.Tutorial;

    /// <summary>
    /// The SqlCommandFactory helpers separate creation of SqlCommand from the Data Gateway for a shorter, one-line look.
    /// This is an MS SQL-only specific helper class and will not apply to other technologies like Cosmos, Mongo, Postgres and etc
    /// </summary>
    public static class TutorialSqlCommandFactory
    {
        public static SqlCommand CreateGetEmployeesCommand()
        {
            var result = new SqlCommand("[dbo].[sp_GetEmployees]")
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60
            };

            return result;
        }

        public static SqlCommand CreateInsertEmployeeCommand(EmployeeEntity employee)
        {
            var result = new SqlCommand("[dbo].[sp_InsertEmployee]")
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60
            };

            result.Parameters.AddWithValue("@firstName", employee.FirstName);
            result.Parameters.AddWithValue("@lastName", employee.LastName);
            result.Parameters.AddWithValue("@address", employee.Address);
            result.Parameters.AddWithValue("@age", employee.Age);
            //// Add all fields as parameters

            return result;
        }
    }
}
