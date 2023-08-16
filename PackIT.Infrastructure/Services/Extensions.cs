using Microsoft.Extensions.DependencyInjection;
using PackIT.Application.Services;

namespace PackIT.Infrastructure.Services;

public static class Extensions
{
    public static IServiceCollection AddWeatherService(this IServiceCollection services)
    {
        services.AddSingleton<IWeatherService, WeatherService>();
        services.AddHttpClient<IWeatherService, WeatherService>(client =>
        {
            client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
        });
        return services;
    }
}