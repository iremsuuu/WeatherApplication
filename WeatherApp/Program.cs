using System;
using System.Net.Http;
using Newtonsoft.Json;

public class WeatherInfo
{
    public string City { get; set; }
    public string Description { get; set; }
    public string Temperature { get; set; }
    public string Humidity { get; set; }
    public string Wind { get; set; }
}

public class WeatherService
{
    private HttpClient _client;

    public WeatherService()
    {
        _client = new HttpClient();
    }

    public WeatherInfo GetWeather(string city)
    {
        try
        {

            string apiUrl = $"https://goweather.herokuapp.com/weather/{city}";
            var response = _client.GetStringAsync(apiUrl).Result;

            
            var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(response);

            
            weatherInfo.City = city;

            return weatherInfo;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata oluştu ({city}): {ex.Message}");
            return null;
        }
    }
}

public class WeatherPrinter
{
    public static void PrintWeather(WeatherInfo weatherInfo)
    {
        if (weatherInfo != null)
        {

            Console.WriteLine($"City: {weatherInfo.City}");
            Console.WriteLine($"Description: {weatherInfo.Description}");
            Console.WriteLine($"Temperature: {weatherInfo.Temperature}");
            Console.WriteLine($"Humidity: {weatherInfo.Humidity}");
            Console.WriteLine($"Wind: {weatherInfo.Wind}");
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        WeatherService weatherService = new WeatherService();

        string[] cities = { "istanbul", "izmir", "ankara" };
        foreach (var city in cities)
        {
            WeatherInfo weatherInfo = weatherService.GetWeather(city);
            WeatherPrinter.PrintWeather(weatherInfo);
        }
    }
}
