namespace Yousource.TurorialApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Yousource.Infrastructure.Services.Interfaces;
    using Yousource.TurorialApi.Extensions;
    using Yousource.TutorialApi.Extensions;
    using Yousource.TutorialApi.Messages.Identity;

    [ApiController]
    [Route("api/tutorial")]
    public class TutorialController : ControllerBase
    {
        private readonly ITutorialService service;

        public TutorialController(ITutorialService service)
        {
            this.service = service;
        }

        [HttpPost()]
        public async Task<IActionResult> InsertEmployeeAsync([FromBody] InsertEmployeeWebRequest request)
        {
            var result = await this.service.InsertEmployeeAsync(request.AsRequest());

            return this.CreateResponse(result.AsWebResponse());
        }

        [HttpGet()]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            var result = await this.service.GetEmployeesAsync();

            return this.CreateResponse(result.AsWebResponse());
        }
    }
}