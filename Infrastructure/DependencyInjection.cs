using Application.Interfaces.External;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddTransient<IBitcoinPriceService, BitcoinPriceService>();

            services
                .AddScoped<IRequestIdService, RequestIdService>();

            services.AddHttpClient();
        }
    }
}