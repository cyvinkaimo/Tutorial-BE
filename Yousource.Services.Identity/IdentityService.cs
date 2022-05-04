namespace Yousource.Services.Identity
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Yousource.ExceptionHandling;
    using Yousource.Infrastructure.Constants.Errors;
    using Yousource.Infrastructure.Entities.Identity;
    using Yousource.Infrastructure.Exceptions;
    using Yousource.Infrastructure.Extensions;
    using Yousource.Infrastructure.Logging;
    using Yousource.Infrastructure.Messages;
    using Yousource.Infrastructure.Messages.Identity;
    using Yousource.Infrastructure.Services.Interfaces;
    using Yousource.Services.Identity.Extensions;
    using Yousource.Services.Identity.Helpers;
    using Yousource.Services.Identity.Validators;
    using Yousource.Services.Identity.Workflows;
    using Yousource.Services.Identity.Workflows.SignInExternal;

    public class IdentityService : IIdentityService, IExceptionHandleable
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<Role> roleManager;
        private readonly JwtHelper jwtHelper;
        private readonly IdentityServiceValidators validators;
        private readonly IdentityServiceWorkflows workflows;

        public IdentityService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            JwtHelper jwtHelper,
            IdentityServiceValidators validators,
            ILogger logger,
            IdentityServiceWorkflows workflows)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.jwtHelper = jwtHelper;
            this.validators = validators;
            this.Logger = logger;
            this.workflows = workflows;
        }

        public ILogger Logger { get; private set; }

        [HandleServiceException(typeof(IdentityServiceException))]
        public async Task<Response> SignUpAsync(SignUpRequest request)
        {
            var result = new Response();

            var validationResult = this.validators.SignUpValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                result.SetError(IdentityServiceErrorCodes.ValidationError, validationResult.ToString(" "));
                return result;
            }

            var identityResult = await this.userManager.CreateAsync(request.AsUser(), request.Password);

            if (!identityResult.Succeeded)
            {
                identityResult.HandleIdentityResultError(ref result);
                return result;
            }

            // Assign Initial Claims from Sign Up
            var createdUser = await this.userManager.FindByNameAsync(request.UserName);
            var claims = new Claim[]
            {
                    new Claim(ClaimTypes.NameIdentifier, createdUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, createdUser.UserName)
            };

            await this.userManager.AddClaimsAsync(createdUser, claims);
            await this.AddToRoleAsync(new AddToRoleRequest { UserId = createdUser.Id.ToString(), Role = request.DefaultRole.ToString() });
            this.Logger.TrackEvent("Sign Up Success", createdUser.CreateLogProperties());

            return result;
        }

        [HandleServiceException(typeof(IdentityServiceException))]
        public async Task<Response<string>> SignInAsync(SignInRequest request)
        {
            var result = new Response<string>();

            var validationResult = this.validators.SignInValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                result.SetError(IdentityServiceErrorCodes.UnexpectedError, validationResult.ToString(" "));
                return result;
            }

            var signInResult = await this.signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

            if (!signInResult.Succeeded)
            {
                // Communicate error as part of the Response
                result.SetError(IdentityServiceErrorCodes.InvalidCredential);
                return result;
            }

            // Generate the JWT Access Token
            var user = await this.userManager.FindByNameAsync(request.UserName);
            var claims = await this.userManager.GetClaimsAsync(user);
            result.Data = this.jwtHelper.GenerateToken(claims);
            this.Logger.TrackEvent("Sign In", user.CreateLogProperties());

            return result;
        }

        [HandleServiceException(typeof(IdentityServiceException))]
        public async Task<Response<string>> SignInExternalAsync(SignInExternalRequest request)
        {
            var result = new Response<string>();

            var payload = this.jwtHelper.ValidateToken(request.IdToken, request.Provider);

            if (payload == null)
            {
                result.SetError(IdentityServiceErrorCodes.GoogleTokenValidationError);
                return result;
            }

            var workflowRequest = new SignInExternalWorkflowRequest(request, result);
            result = await this.workflows.CreateSignInExternalWorkflow().ExecuteAsync(workflowRequest);

            this.Logger.TrackEvent("Sign In External", workflowRequest.User.CreateLogProperties());

            return result;
        }

        [HandleServiceException(typeof(IdentityServiceException))]
        public async Task<Response> AddToRoleAsync(AddToRoleRequest request)
        {
            var result = new Response();

            result = await this.roleManager.AddToRoleAsync(this.userManager, request);

            return result;
        }
    }
}
