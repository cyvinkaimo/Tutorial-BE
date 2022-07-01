namespace Yousource.Services.Tutorial.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Yousource.ExceptionHandling;
    using Yousource.Infrastructure.Data.Interfaces;
    using Yousource.Infrastructure.Entities.Customers;
    using Yousource.Infrastructure.Entities.Tutorial;
    using Yousource.Infrastructure.Exceptions;
    using Yousource.Infrastructure.Helpers;
    using Yousource.Infrastructure.Logging;
    using Yousource.Infrastructure.Services.Interfaces;

    //// Data access class
    public class TutorialSqlDataGateway : ITutorialDataGateway, IExceptionHandleable
    {
        private readonly ISqlHelper sql;

        //// Inject necessary data access adapter like `ISqlHelper` and `ILogger`
        //// Inject command factory; Separates the creation of commands with parameters to be executed
        public TutorialSqlDataGateway(
            ISqlHelper helper, 
            ILogger logger)
        {
            this.sql = helper;
            this.Logger = logger;
        }

        public ILogger Logger { get; private set; }

        //// Attribute that catches and wraps exceptions as data exceptions then rethrows them
        [HandleDataException(typeof(DataException))]
        public async Task<IEnumerable<EmployeeEntity>> GetEmployeesAsync()
        {
            var result = new List<EmployeeEntity>();

            var command = TutorialSqlCommandFactory.CreateGetEmployeesCommand();
            result = await this.sql.ReadAsListAsync<EmployeeEntity>(command);

            return result;
        }

        //// Attribute that catches and wraps exceptions as data-related exceptions then rethrows them
        [HandleDataException(typeof(DataException))]
        public async Task InsertEmployeeAsync(EmployeeEntity employee)
        {
            var command = TutorialSqlCommandFactory.CreateInsertEmployeeCommand(employee);
            await this.sql.ExecuteAsync(command);
        }
    }
}
