namespace Yousource.Api.Extensions.Injection
{
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Yousource.Infrastructure.Entities.Identity;
    using Yousource.Infrastructure.Services;
    using Yousource.Infrastructure.Settings;
    using Yousource.Services.Identity;
    using Yousource.Services.Identity.Data;

    public static class IdentityInjection
    {
        /// <summary>
        /// Provisions a very basic authentication using Username-Password and JWT
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
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
                // Configure custom Identity Options here such as Password Rules, Sign In Rules, and etc.
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

            services.AddSingleton(new JwtSettings(jwtConfig["Secret"]));
            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
