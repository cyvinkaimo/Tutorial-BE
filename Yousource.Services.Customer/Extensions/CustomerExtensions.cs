namespace Yousource.Services.Customer.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Yousource.Infrastructure.Messages.Customers.Requests;
    using Yousource.Infrastructure.Models.Customers;
    using Yousource.Services.Customer.Data.Entities;

    public static class CustomerExtensions
    {
        public static CustomerEntity AsEntity(this CreateCustomerRequest request)
        {
            var result = new CustomerEntity
            {
                Email = request.Email,
                Name = request.Name
                //// Assign all necessary fields e.g. when new fields are introduced
            };

            return result;
        }

        public static IEnumerable<Customer> AsModel(this IEnumerable<CustomerEntity> entities)
        {
            var result = entities.Select(entity => entity.AsModel());
            return result;
        }

        public static Customer AsModel(this CustomerEntity entity)
        {
            var result = new Customer
            {
                Email = entity.Email,
                Name = entity.Name
                //// Assign all necessary fields e.g. when new fields are introduced
            };

            return result;
        }
    }
}
