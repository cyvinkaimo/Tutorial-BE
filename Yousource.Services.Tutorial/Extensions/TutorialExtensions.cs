namespace Yousource.Services.Tutorial.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Yousource.Infrastructure.Entities.Tutorial;
    using Yousource.Infrastructure.Messages.Tutorial.Requests;
    using Yousource.Infrastructure.Models.Tutorial;

    public static class TutorialExtensions
    {
        public static Employee AsModel(this EmployeeEntity entity)
        {
            return new Employee
            {
                Address = entity.Address,
                Age = entity.Age,
                FirstName = entity.FirstName,
                Id = entity.Id,
                LastName = entity.LastName
            };
        }

        public static IEnumerable<Employee> AsModel(this IEnumerable<EmployeeEntity> entities)
        {
            return entities.Select(entity => entity.AsModel());
        }

        public static EmployeeEntity AsEntity(this InsertEmployeeRequest model)
        {
            return new EmployeeEntity
            {
                Address = model.Address,
                Age = model.Age,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }
    }
}
