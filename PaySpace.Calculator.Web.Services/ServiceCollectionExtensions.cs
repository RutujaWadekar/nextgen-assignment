using Microsoft.Extensions.DependencyInjection;

using PaySpace.Calculator.Web.Services.Abstractions;

namespace PaySpace.Calculator.Web.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddHttpClient<ICalculatorHttpService, CalculatorHttpService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7119/"); // Adjust API URL if needed
            });
            return services;
        }
    }
}