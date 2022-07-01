namespace Yousource.Infrastructure.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Yousource.Infrastructure.Messages;
    using Yousource.Infrastructure.Messages.Tutorial.Requests;
    using Yousource.Infrastructure.Models.Tutorial;

    public interface ITutorialService
    {
        Task<Response<IEnumerable<Employee>>> GetEmployeesAsync();

        Task<Response> InsertEmployeeAsync(InsertEmployeeRequest request);
    }
}
