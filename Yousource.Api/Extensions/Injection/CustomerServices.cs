﻿namespace Yousource.Api.Extensions.Injection
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Yousource.Infrastructure.Interfaces;
    using Yousource.Services.Customer;
    using Yousource.Services.Customer.Data;

    public static class CustomerServices
    {
        public static void InjectCustomerService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<ICustomerSqlCommandFactory, CustomerSqlCommandFactory>();
            services.AddSingleton<ICustomerDataGateway, CustomerSqlDataGateway>();
        }
    }
}
