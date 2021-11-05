namespace Yousource.Services.Customer.Data
{
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Threading.Tasks;
    using Yousource.Infrastructure.Data;
    using Yousource.Infrastructure.Entities.Customers;
    using Yousource.Infrastructure.Helpers;
    using Yousource.Infrastructure.Logging;
    using Yousource.Services.Customer.Exceptions;

    //// Data access class
    public class CustomerSqlDataGateway : ICustomerDataGateway
    {
        private readonly ISqlHelper sql;
        private readonly ILogger logger;

        //// Inject necessary data access adapter like `ISqlHelper` and `ILogger`
        //// Inject command factory; Separates the creation of commands with parameters to be executed
        public CustomerSqlDataGateway(
            ISqlHelper helper, 
            ILogger logger)
        {
            this.sql = helper;
            this.logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var result = new List<Customer>();

            try
            {
                var command = CustomerSqlCommandFactory.CreateGetCustomersCommand();
                result = await this.sql.ReadAsListAsync<Customer>(command);
            }
            catch (DbException ex)
            {
                //// Log, Wrap and Rethrow data-related exceptions
                this.logger.WriteException(ex);
                throw new CustomerDataException(ex);
            }

            return result;
        }

        public async Task InsertCustomerAsync(Customer customer)
        {
            try
            {
                var command = CustomerSqlCommandFactory.CreateInsertCustomerCommand(customer);
                await this.sql.ExecuteAsync(command);
            }
            catch (DbException ex)
            {
                //// Log, Wrap and Rethrow data-related exceptions
                this.logger.WriteException(ex);
                throw new CustomerDataException(ex);
            }
        }
    }
}
