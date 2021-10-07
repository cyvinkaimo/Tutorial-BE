namespace Yousource.Api.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Yousource.Api.Extensions;
    using Yousource.Api.Extensions.Customer;
    using Yousource.Api.Filters;
    using Yousource.Api.Messages.Customers.Requests;
    using Yousource.Infrastructure.Services;

    /// <summary>
    /// Represent each Frontend Page as a Controller for one-to-one modeling/structuring
    /// </summary>
    [Route("api/create_customer")]
    [TypeFilter(typeof(ValidateModelStateAttribute))]
    [TypeFilter(typeof(LogExceptionAttribute))]
    public class CreateCustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        //// Inject controller dependencies. Usually services
        public CreateCustomerController(ICustomerService customerService)
        {
            Debug.Assert(customerService != null, "Null dependencies");
            this.customerService = customerService;
        }

        [HttpPost]
        [AllowAnonymous]
        //// Controller code should only contain two lines i.e. invocation of service
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerWebRequest request)
        {
            //// Decouple models/request-response from Api and Service layer
            //// Create Extension `.AsRequest` to convert models.
            var result = await this.customerService.CreateCustomerAsync(request.AsRequest());
            return this.CreateResponse(result.AsWebResponse());
        }
    }
}
