namespace Yousource.TutorialApi.Extensions.Injection
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Yousource.Infrastructure.Services.Interfaces;
    using Yousource.Services.Tutorial;

    public static class TutorialServiceInjection
    {
        public static void ConfigureTutorialService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITutorialService, TutorialService>();
        }
    }
}
