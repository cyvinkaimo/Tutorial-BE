namespace Yousource.Services.Customer
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Yousource.ExceptionHandling;
    using Yousource.Infrastructure.Data.Interfaces;
    using Yousource.Infrastructure.Exceptions;
    using Yousource.Infrastructure.Logging;
    using Yousource.Infrastructure.Messages.Customers.Requests;
    using Yousource.Infrastructure.Messages.Customers.Responses;
    using Yousource.Infrastructure.Services.Interfaces;
    using Yousource.Services.Customer.Constants;
    using Yousource.Services.Customer.Extensions;
    using Yousource.Services.Customer.Specifications;

    //// Service typically executes the operations needed e.g. CRUD.
    public class CustomerService : ICustomerService, IExceptionHandleable
    {
        private readonly ICustomerDataGateway dataGateway;

        //// Inject dependencies. Usually data access dependencies i.e. Data Gateways
        public CustomerService(
            ICustomerDataGateway dataGateway,
            ILogger logger)
        {
            this.dataGateway = dataGateway;
            this.Logger = logger;
        }

        public ILogger Logger { get; private set; }

        //// Attribute that catches and wraps exceptions as service-related exceptions then rethrows them
        [HandleServiceException(typeof(CustomerServiceException))]
        public async Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest request)
        {
            var result = new CreateCustomerResponse();
            var errors = new List<string>() as ICollection<string>;

            //// Validate using Specification classes. You can leverage factories to inject
            //// your specifications if it touches the database
            var spec = new ValidCreateCustomerRequestSpecification();
            if (spec.IsSatisfiedBy(request, ref errors))
            {
                //// Decouple Service Models from Shared models i.e. Create `AsEntity` extension to convert vice-versa
                await this.dataGateway.InsertCustomerAsync(request.AsEntity());
            }
            else
            {
                //// Communicate Specification-added errors, and return appropriate error.
                result.SetError(Errors.CreateValidationError, errors);
            }

            return result;
        }

        //// Attribute that catches and wraps exceptions as service-related exceptions then rethrows them
        [HandleServiceException(typeof(CustomerServiceException))]
        public async Task<GetCustomersResponse> GetCustomersAsync()
        {
            var result = new GetCustomersResponse();

            //// Convert Entity (Service) model to Shared (Infra) model using `AsModel` extension
            result.Data = (await this.dataGateway.GetCustomersAsync()).AsModel();

            return result;
        }
    }
}
