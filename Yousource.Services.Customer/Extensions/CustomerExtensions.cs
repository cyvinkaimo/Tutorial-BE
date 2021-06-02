namespace Yousource.Services.Customer.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Yousource.Infrastructure.Messages.Customers.Requests;

    public static class CustomerExtensions
    {
        public static Infrastructure.Entities.Customers.Customer AsEntity(this CreateCustomerRequest request)
        {
            var result = new Infrastructure.Entities.Customers.Customer
            {
                Email = request.Email,
                Name = request.Name
                //// Assign all necessary fields e.g. when new fields are introduced
            };

            return result;
        }

        public static IEnumerable<Infrastructure.Models.Customers.Customer> AsModel(this IEnumerable<Infrastructure.Entities.Customers.Customer> entities)
        {
            var result = entities.Select(entity => entity.AsModel());
            return result;
        }

        public static Infrastructure.Models.Customers.Customer AsModel(this Infrastructure.Entities.Customers.Customer entity)
        {
            var result = new Infrastructure.Models.Customers.Customer
            {
                Email = entity.Email,
                Name = entity.Name
                //// Assign all necessary fields e.g. when new fields are introduced
            };

            return result;
        }
    }
}
