namespace Yousource.Infrastructure.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Yousource.Infrastructure.Entities.Tutorial;

    public interface ITutorialDataGateway
    {
        Task<IEnumerable<EmployeeEntity>> GetEmployeesAsync();

        Task InsertEmployeeAsync(EmployeeEntity employee);
    }
}
