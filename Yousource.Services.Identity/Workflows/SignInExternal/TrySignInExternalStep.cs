namespace Yousource.Services.Identity.Workflows.SignInExternal
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Yousource.Infrastructure.Entities.Identity;
    using Yousource.Infrastructure.Messages;
    using Yousource.Infrastructure.Workflows;
    using Yousource.Services.Identity.Extensions;
    using Yousource.Services.Identity.Helpers;

    internal class TrySignInExternalStep : AsyncStep<SignInExternalWorkflowRequest, Response<string>>
    {
        private readonly UserManager<User> userManager;
        private readonly JwtHelper jwtHelper;

        public TrySignInExternalStep(UserManager<User> userManager, JwtHelper jwtHelper)
        {
            this.userManager = userManager;
            this.jwtHelper = jwtHelper;
        }

        public override async Task<Response<string>> ExecuteAsync(SignInExternalWorkflowRequest request)
        {
            var info = new UserLoginInfo(request.Request.Provider, request.Payload.Subject, request.Request.Provider);
            request.UserLoginInfo = info;

            var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (user == null)
            {
                return await ExecuteNextAsync(request);
            }

            request.User = user;
            request.Response.Data = await userManager.GenerateJwtAsync(jwtHelper, user);
            return request.Response;
        }
    }
}
