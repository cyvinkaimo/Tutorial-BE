namespace Yousource.TutorialApi.Extensions.Injection
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Yousource.Infrastructure.Data.Interfaces;
    using Yousource.Infrastructure.Services.Interfaces;
    using Yousource.Services.Tutorial;
    using Yousource.Services.Tutorial.Data;

    public static class TutorialServiceInjection
    {
        public static void ConfigureTutorialService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITutorialService, TutorialService>();
            services.AddScoped<ITutorialDataGateway, TutorialSqlDataGateway>();
        }
    }
}
