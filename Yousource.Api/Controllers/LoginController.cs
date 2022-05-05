namespace Yousource.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Yousource.Api.Extensions;
    using Yousource.Api.Filters;
    using Yousource.Api.Messages.Identity;
    using Yousource.Infrastructure.Enums.Identity;
    using Yousource.Infrastructure.Services.Interfaces;

    [Route("api/login")]
    [TypeFilter(typeof(LogExceptionAttribute))]
    [TypeFilter(typeof(ValidateModelStateAttribute))]
    [AllowAnonymous] //// NOTE that your other controllers must not have this attribute. The Login page is meant to be public
    //// [Authorize] Use the authorize attribute for other controllers to validate JWT/access token
    public class LoginController : ControllerBase
    {
        private readonly IIdentityService identityService;

        public LoginController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost("external")]
        public async Task<IActionResult> SignInExternalAsync([FromBody] SignInExternalWebRequest request)
        {
            var result = await this.identityService.SignInExternalAsync(request.AsRequest(Role.Employer));
            return this.CreateResponse(result.AsSignInExternalWebResponse());
        }

        [HttpPost]
        public async Task<IActionResult> SignInAsync([FromBody] SignInWebRequest request)
        {
            var result = await this.identityService.SignInAsync(request.AsRequest());
            return this.CreateResponse(result.AsSignInWebResponse());
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpWebRequest request)
        {
            var result = await this.identityService.SignUpAsync(request.AsRequest(Role.Employer));
            return this.CreateResponse(result.AsSignUpWebResponse());
        }
    }
}
