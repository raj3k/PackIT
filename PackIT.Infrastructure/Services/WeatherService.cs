using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using PackIT.Application.Dto.External;
using PackIT.Application.Services;
using PackIT.Domain.ValueObjects;

namespace PackIT.Infrastructure.Services;

public class WeatherService : IWeatherService
{
    private const string OpenWeatherMapApiKey = "";
    private const string TemperatureUnit = "metric";

    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherDto> GetWeatherAsync(Localization localization)
    {
        HttpResponseMessage response = await _httpClient
            .GetAsync($"weather?q={localization.City}&appid={OpenWeatherMapApiKey}&units={TemperatureUnit}");

        response.EnsureSuccessStatusCode();

        string responseJson = await response.Content.ReadAsStringAsync();
        
        return await Task.FromResult(new WeatherDto(GetTempFromResponse(responseJson)));
        
        // WeatherModelData? jsonData = await response.Content.ReadFromJsonAsync<WeatherModelData>();
        //
        // return await Task.FromResult(new WeatherDto(jsonData.Main.Temp));
    }

    private double GetTempFromResponse(string responseJson)
    {
        JObject data = JObject.Parse(responseJson);

        var temp = data["main"]?["temp"];

        return Convert.ToDouble(temp);
    }
}
