namespace Yousource.Api.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Yousource.Api.Extensions;
    using Yousource.Api.Extensions.Identity;
    using Yousource.Api.Filters;
    using Yousource.Api.Messages.Identity;
    using Yousource.Infrastructure.Services;

    [Route("api/login")]
    [TypeFilter(typeof(LogExceptionAttribute))]
    [TypeFilter(typeof(ValidateModelStateAttribute))]
    public class LoginController : ControllerBase
    {
        private readonly IIdentityService identityService;

        public LoginController(IIdentityService identityService)
        {
            Debug.Assert(identityService != null, "Null dependencies");
            this.identityService = identityService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync([FromBody] SignInWebRequest request)
        {
            var result = await this.identityService.SignInAsync(request.AsRequest());
            return this.CreateResponse(result.AsWebResponse());
        }

        [HttpPost("sign_up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpWebRequest request)
        {
            var result = await this.identityService.SignUpAsync(request.AsRequest());
            return this.CreateResponse(result.AsWebResponse());
        }
    }
}
