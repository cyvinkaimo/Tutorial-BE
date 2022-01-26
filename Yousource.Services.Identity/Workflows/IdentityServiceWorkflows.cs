namespace Yousource.Services.Identity.Workflows
{
    using Microsoft.AspNetCore.Identity;
    using Yousource.Infrastructure.Entities.Identity;
    using Yousource.Infrastructure.Messages;
    using Yousource.Infrastructure.Workflows;
    using Yousource.Services.Identity.Helpers;
    using Yousource.Services.Identity.Workflows.SignInExternal;

    public class IdentityServiceWorkflows
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly JwtHelper jwtHelper;

        public IdentityServiceWorkflows(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            JwtHelper jwtHelper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtHelper = jwtHelper;
        }

        public virtual AsyncStep<SignInExternalWorkflowRequest, Response<string>> CreateSignInExternalWorkflow()
        {
            var result = new ValidateExternalTokenStep(jwtHelper);
            result.SetNextStep(new TrySignInExternalStep(userManager, jwtHelper));
            result.SetNextStep(new SignUpExternalStep(userManager, roleManager, jwtHelper));
            return result;
        }
    }
}
