namespace Yousource.Services.Customer.Specifications
{
    using System.Collections.Generic;
    using Yousource.Infrastructure.Messages.Customers.Requests;
    using Yousource.Infrastructure.Specifications;

    public class ValidCreateCustomerRequestSpecification : Specification<CreateCustomerRequest>
    {
        public override bool IsSatisfiedBy(CreateCustomerRequest entity, ref ICollection<string> errors)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                errors.Add("Customer name is required");
            }

            var result = errors.Count == 0;
            return result;
        }
    }
}
