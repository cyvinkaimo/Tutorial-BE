namespace Yousource.Services.Customer.Data
{
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;
    using Yousource.Infrastructure.Entities.Customers;
    using Yousource.Infrastructure.Helpers;
    using Yousource.Infrastructure.Logging;
    using Yousource.Services.Customer.Exceptions;

    //// Data access class
    public class CustomerSqlDataGateway : ICustomerDataGateway
    {
        private readonly ISqlHelper sql;
        private readonly ILogger logger;
        private readonly ICustomerSqlCommandFactory factory;

        //// Inject necessary data access adapter like `ISqlHelper` and `ILogger`
        //// Inject command factory; Separates the creation of commands with parameters to be executed
        public CustomerSqlDataGateway(
            ISqlHelper helper, 
            ILogger logger,
            ICustomerSqlCommandFactory factory)
        {
            this.sql = helper;
            this.logger = logger;
            this.factory = factory;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var result = new List<Customer>();

            try
            {
                var command = this.factory.CreateGetCustomersCommand();
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
                var command = this.factory.CreateInsertCustomerCommand(customer);
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
