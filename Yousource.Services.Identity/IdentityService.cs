namespace Yousource.Services.Identity
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using Yousource.Infrastructure.Constants;
    using Yousource.Infrastructure.Entities.Identity;
    using Yousource.Infrastructure.Messages.Identity;
    using Yousource.Infrastructure.Services;
    using Yousource.Infrastructure.Settings;
    using Yousource.Services.Identity.Exceptions;
    using Yousource.Services.Identity.Extensions;

    /// <summary>
    /// Implemented using Microsoft AspNet Core Identity Framework
    /// </summary>
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly JwtSettings jwtSettings;

        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager, JwtSettings jwtSecret)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtSettings = jwtSecret;
        }

        public async Task<SignUpResponse> SignUpAsync(SignUpRequest request)
        {
            var result = new SignUpResponse();

            try
            {
                var identityResult = await this.userManager.CreateAsync(request.AsUser(), request.Password);

                if (!identityResult.Succeeded)
                {
                    return result;
                }

                // Assign Initial Claims from Sign Up
                var createdUser = await this.userManager.FindByNameAsync(request.UserName);
                var claims = new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, createdUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, createdUser.UserName)
                };

                await this.userManager.AddClaimsAsync(createdUser, claims);
            }
            catch (Exception ex)
            {
                throw new IdentityException(ex);
            }

            return result;
        }

        public async Task<SignInResponse> SignInAsync(SignInRequest request)
        {
            var result = new SignInResponse();

            try
            {
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

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this.jwtSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(this.jwtSettings.ExpiresInMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                result.AccessToken = tokenHandler.WriteToken(token);
                result.Expires = tokenDescriptor.Expires;
            }
            catch (Exception ex)
            {
                throw new IdentityException(ex);
            }

            return result;
        }
    }
}
