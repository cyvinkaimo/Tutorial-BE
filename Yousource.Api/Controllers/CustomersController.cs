namespace Yousource.Api.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Yousource.Api.Extensions;
    using Yousource.Api.Extensions.Customer;
    using Yousource.Api.Filters;
    using Yousource.Infrastructure.Services;

    //// Use the Page Controllers or Experience Controllers convention wherein
    //// we create controllers per "pages/experience" and not in a RESTful manner
    [Route("api/customers")]
    [TypeFilter(typeof(ValidateModelStateAttribute))]
    [TypeFilter(typeof(LogExceptionAttribute))]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        //// Inject controller dependencies. Usually services
        public CustomersController(ICustomerService customerService)
        {
            Debug.Assert(customerService != null, "Null dependencies");
            this.customerService = customerService;
        }

        [HttpGet]
        [AllowAnonymous]
        //// Controller code should only contain two lines i.e. invocation of service
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await this.customerService.GetCustomersAsync();
            return this.CreateResponse(result.AsWebResponse());
        }
    }
}
