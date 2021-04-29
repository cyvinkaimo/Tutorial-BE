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

    //// Use the Page Controllers or Experience Controllers convention wherein
    //// we create controllers per "pages/experience" and not in a RESTful manner
    [Route("api/create_customer")]
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
        [ServiceFilter(typeof(ValidateModelStateAttribute))]
        [ServiceFilter(typeof(LogExceptionAttribute))]
        //// Controller code should only contain two lines i.e. invocation of service
        public async Task<IActionResult> AddCustomerAsync([FromBody] AddCustomerWebRequest request)
        {
            //// Decouple models/request-response from Api and Service layer
            //// Create Extension `.AsRequest` to convert models.
            var result = await this.customerService.CreateCustomerAsync(request.AsRequest());
            return this.CreateResponse(result.AsWebResponse());
        }
    }
}
