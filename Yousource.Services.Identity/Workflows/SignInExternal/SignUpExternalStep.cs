﻿namespace Yousource.Services.Identity.Workflows.SignInExternal
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Yousource.Infrastructure.Entities.Identity;
    using Yousource.Infrastructure.Messages.Identity;
    using Yousource.Infrastructure.Messages;
    using Yousource.Infrastructure.Workflows;
    using Yousource.Services.Identity.Extensions;
    using Yousource.Services.Identity.Helpers;
    using Yousource.Infrastructure.Constants.Errors;

    internal class SignUpExternalStep : AsyncStep<SignInExternalWorkflowRequest, Response<string>>
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly JwtHelper jwtHelper;

        public SignUpExternalStep(UserManager<User> userManager, RoleManager<Role> roleManager, JwtHelper jwtHelper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtHelper = jwtHelper;
        }

        public override async Task<Response<string>> ExecuteAsync(SignInExternalWorkflowRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Payload.Email);

            if (user == null)
            {
                user = new User { Email = request.Payload.Email, UserName = request.Payload.Email };
                await userManager.CreateAsync(user);
                await userManager.AddLoginAsync(user, request.UserLoginInfo);

                var defaultClaims = request.Payload.AsClaims(user.Id.ToString());
                await userManager.AddClaimsAsync(user, defaultClaims);
                await roleManager.AddToRoleAsync(userManager, new AddToRoleRequest { UserId = user.Id.ToString(), Role = request.Request.DefaultRole.ToString() });

                request.User = user;
                request.Response.Data = await userManager.GenerateJwtAsync(jwtHelper, user);
                return request.Response;
            }
            else
            {
                request.Response.SetError(IdentityServiceErrorCodes.GoogleEmailAlreadyInUse);
                return request.Response;
            }
        }
    }
}
