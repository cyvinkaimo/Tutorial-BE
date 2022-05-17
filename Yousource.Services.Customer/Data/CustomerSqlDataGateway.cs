namespace Yousource.Services.Customer.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Yousource.ExceptionHandling;
    using Yousource.Infrastructure.Data.Interfaces;
    using Yousource.Infrastructure.Entities.Customers;
    using Yousource.Infrastructure.Exceptions;
    using Yousource.Infrastructure.Helpers;
    using Yousource.Infrastructure.Logging;
    using Yousource.Infrastructure.Services.Interfaces;

    //// Data access class
    public class CustomerSqlDataGateway : ICustomerDataGateway, IExceptionHandleable
    {
        private readonly ISqlHelper sql;

        //// Inject necessary data access adapter like `ISqlHelper` and `ILogger`
        //// Inject command factory; Separates the creation of commands with parameters to be executed
        public CustomerSqlDataGateway(
            ISqlHelper helper, 
            ILogger logger)
        {
            this.sql = helper;
            this.Logger = logger;
        }

        public ILogger Logger { get; private set; }

        //// Attribute that catches and wraps exceptions as data exceptions then rethrows them
        [HandleDataException(typeof(DataException))]
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var result = new List<Customer>();

            var command = CustomerSqlCommandFactory.CreateGetCustomersCommand();
            result = await this.sql.ReadAsListAsync<Customer>(command);

            return result;
        }

        //// Attribute that catches and wraps exceptions as data-related exceptions then rethrows them
        [HandleDataException(typeof(DataException))]
        public async Task InsertCustomerAsync(Customer customer)
        {
            var command = CustomerSqlCommandFactory.CreateInsertCustomerCommand(customer);
            await this.sql.ExecuteAsync(command);
        }
    }
}
