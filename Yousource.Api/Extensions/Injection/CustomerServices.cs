namespace Yousource.Api.Extensions.Injection
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Yousource.Infrastructure.Data;
    using Yousource.Infrastructure.Services;
    using Yousource.Services.Customer;
    using Yousource.Services.Customer.Data;

    public static class CustomerServices
    {
        public static void InjectCustomerService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerDataGateway, CustomerSqlDataGateway>();
        }
    }
}
