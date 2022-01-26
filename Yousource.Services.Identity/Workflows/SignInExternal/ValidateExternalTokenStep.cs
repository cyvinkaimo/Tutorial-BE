namespace Yousource.Services.Identity.Workflows.SignInExternal
{
    using System.Threading.Tasks;
    using Yousource.Infrastructure.Constants.Errors;
    using Yousource.Infrastructure.Messages;
    using Yousource.Infrastructure.Workflows;
    using Yousource.Services.Identity.Helpers;

    internal class ValidateExternalTokenStep : AsyncStep<SignInExternalWorkflowRequest, Response<string>>
    {
        private readonly JwtHelper jwtHelper;

        public ValidateExternalTokenStep(JwtHelper jwtHelper)
        {
            this.jwtHelper = jwtHelper;
        }

        public override async Task<Response<string>> ExecuteAsync(SignInExternalWorkflowRequest request)
        {
            var payload = this.jwtHelper.ValidateToken(request.Request.IdToken, request.Request.Provider);

            if (payload == null)
            {
                request.Response.SetError(IdentityServiceErrorCodes.GoogleTokenValidationError);
                return request.Response;
            }

            request.Payload = payload;
            return await this.ExecuteNextAsync(request);
        }
    }
}
