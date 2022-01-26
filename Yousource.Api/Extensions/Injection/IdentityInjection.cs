namespace Yousource.Api.Extensions.Injection
{
    using System.Text;
    using FluentValidation;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Yousource.Infrastructure.Entities.Identity;
    using Yousource.Infrastructure.Messages.Identity;
    using Yousource.Infrastructure.Services.Interfaces;
    using Yousource.Infrastructure.Settings;
    using Yousource.Services.Identity;
    using Yousource.Services.Identity.Data;
    using Yousource.Services.Identity.Helpers;
    using Yousource.Services.Identity.Validators;
    using Yousource.Services.Identity.Validators.SignIn;
    using Yousource.Services.Identity.Validators.SignUp;
    using Yousource.Services.Identity.Workflows;

    public static class IdentityInjection
    {
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    config.GetSection("Database")["ConnectionString"]));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            var jwtConfig = config.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtConfig["Secret"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            var jwtSettings = new JwtSettings(jwtConfig["Secret"]);
            var jwtHelper = new JwtHelper(config.GetSection("Google")["ClientId"], jwtSettings);
            services.AddSingleton(jwtSettings);
            services.AddScoped(typeof(JwtHelper), instance => jwtHelper);
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped(typeof(IdentityServiceWorkflows));

            //// Inject validators
            services.AddScoped<IValidator<SignUpRequest>, SignUpValidator>();
            services.AddScoped<IValidator<SignInRequest>, SignInValidator>();
            services.AddScoped(typeof(IdentityServiceValidators));
        }
    }
}
