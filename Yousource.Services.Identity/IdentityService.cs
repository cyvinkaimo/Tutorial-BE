namespace Yousource.Services.Identity
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
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

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<Role> roleManager;
        private readonly JwtHelper jwtHelper;
        private readonly IdentityServiceValidators validators;
        private readonly ILogger logger;
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
            this.logger = logger;
            this.workflows = workflows;
        }

        public async Task<Response> SignUpAsync(SignUpRequest request)
        {
            var result = new Response();

            try
            {
                var validationResult = validators.SignUpValidator.Validate(request);

                if (!validationResult.IsValid)
                {
                    result.SetError(IdentityServiceErrorCodes.ValidationError, validationResult.ToString(" "));
                    return result;
                }

                var identityResult = await userManager.CreateAsync(request.AsUser(), request.Password);

                if (!identityResult.Succeeded)
                {
                    identityResult.HandleIdentityResultError(ref result);
                    return result;
                }

                // Assign Initial Claims from Sign Up
                var createdUser = await userManager.FindByNameAsync(request.UserName);
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, createdUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, createdUser.UserName)
                };

                await userManager.AddClaimsAsync(createdUser, claims);
                await AddToRoleAsync(new AddToRoleRequest { UserId = createdUser.Id.ToString(), Role = request.DefaultRole.ToString() });
                logger.TrackEvent("Sign Up Success", createdUser.CreateLogProperties());
            }
            catch (Exception ex)
            {
                logger.WriteException(ex);
                throw new IdentityServiceException(ex);
            }

            return result;
        }

        public async Task<Response<string>> SignInAsync(SignInRequest request)
        {
            var result = new Response<string>();

            try
            {
                var validationResult = validators.SignInValidator.Validate(request);

                if (!validationResult.IsValid)
                {
                    result.SetError(IdentityServiceErrorCodes.UnexpectedError, validationResult.ToString(" "));
                    return result;
                }

                var signInResult = await signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

                if (!signInResult.Succeeded)
                {
                    // Communicate error as part of the Response
                    result.SetError(IdentityServiceErrorCodes.InvalidCredential);
                    return result;
                }

                // Generate the JWT Access Token
                var user = await userManager.FindByNameAsync(request.UserName);
                var claims = await userManager.GetClaimsAsync(user);
                result.Data = jwtHelper.GenerateToken(claims);
                logger.TrackEvent("Sign In", user.CreateLogProperties());
            }
            catch (Exception ex)
            {
                logger.WriteException(ex);
                throw new IdentityServiceException(ex);
            }

            return result;
        }

        public async Task<Response<string>> SignInExternalAsync(SignInExternalRequest request)
        {
            var result = new Response<string>();

            try
            {
                var payload = jwtHelper.ValidateToken(request.IdToken, request.Provider);

                if (payload == null)
                {
                    result.SetError(IdentityServiceErrorCodes.GoogleTokenValidationError);
                    return result;
                }

                var workflowRequest = new SignInExternalWorkflowRequest(request, result);
                result = await workflows.CreateSignInExternalWorkflow().ExecuteAsync(workflowRequest);

                logger.TrackEvent("Sign In External", workflowRequest.User.CreateLogProperties());
            }
            catch (Exception ex)
            {
                logger.WriteException(ex);
                throw new IdentityServiceException(ex);
            }

            return result;
        }

        public async Task<Response> AddToRoleAsync(AddToRoleRequest request)
        {
            var result = new Response();

            try
            {
                result = await roleManager.AddToRoleAsync(userManager, request);
            }
            catch (Exception ex)
            {
                throw new IdentityServiceException(ex);
            }

            return result;
        }
    }
}
