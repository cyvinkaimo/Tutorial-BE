namespace Yousource.Services.Identity
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using Yousource.Infrastructure.Messages.Identity;
    using Yousource.Services.Identity.Entities;
    using Yousource.Services.Identity.Exceptions;
    using Yousource.Services.Identity.Extensions;

    public class IdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly string jwtSecret;

        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager, string jwtSecret)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtSecret = jwtSecret;
        }

        public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
        {
            var result = new CreateUserResponse();

            try
            {
                var identityResult = await this.userManager.CreateAsync(request.AsUser(), request.Password);

                if (!identityResult.Succeeded)
                {
                    // Communicate error as part of the Response
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
                    return result;
                }

                // Generate the JWT Access Token
                var user = await this.userManager.FindByNameAsync(request.UserName);
                var claims = await this.userManager.GetClaimsAsync(user);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this.jwtSecret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(request.ExpiresInMinutes),
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
