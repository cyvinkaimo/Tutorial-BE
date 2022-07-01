namespace Yousource.Services.Tutorial.Specifications
{
    using System.Collections.Generic;
    using Yousource.Infrastructure.Messages.Tutorial.Requests;
    using Yousource.Infrastructure.Specifications;

    public class ValidInsertEmployeeRequestSpecification : Specification<InsertEmployeeRequest>
    {
        public override bool IsSatisfiedBy(InsertEmployeeRequest entity, ref ICollection<string> errors)
        {
            if (string.IsNullOrEmpty(entity.FirstName))
            {
                errors.Add("Employee firtst name is required");
            }

            if (string.IsNullOrEmpty(entity.LastName))
            {
                errors.Add("Employee last name is required");
            }

            if (entity.Age <= 18)
            {
                errors.Add("Employee age is required");
            }

            if (string.IsNullOrEmpty(entity.Address))
            {
                errors.Add("Employee address is required");
            }

            var result = errors.Count == 0;
            return result;
        }
    }
}
