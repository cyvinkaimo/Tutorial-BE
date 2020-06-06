namespace Yousource.Api.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Yousource.Api.Extensions;
    using Yousource.Api.Extensions.Customer;
    using Yousource.Api.Filters;
    using Yousource.Infrastructure.Interfaces;

    //// Use the Page Controllers or Experience Controllers convention wherein
    //// we create controllers per "pages/experience" and not in a RESTful manner
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService service;

        //// Inject controller dependencies. Usually services
        public CustomersController(ICustomerService service)
        {
            Debug.Assert(service != null, "Null dependencies");
            this.service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        [ServiceFilter(typeof(ValidateModelStateAttribute))]
        [ServiceFilter(typeof(LogExceptionAttribute))]
        //// Controller code should only contain two lines i.e. invocation of service
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await this.service.GetCustomersAsync();
            return this.CreateResponse(result.AsWebResponse());
        }
    }
}
