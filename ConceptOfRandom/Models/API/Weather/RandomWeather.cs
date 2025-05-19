using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConceptOfRandom.Models.API;

public class RandomWeather : APIModel {
    public new readonly string Name = "Weather";
    private const string url = "https://goweather.xyz/weather/";
    public new string Parameters;

    public class WeatherForecast
    {
        [JsonPropertyName("temperature")]
        public string Temperature { get; init; }

        [JsonPropertyName("wind")]
        public string Wind { get; init; }

        [JsonPropertyName("description")]
        public string Description { get; init; }

        [JsonPropertyName("forecast")]
        public List<ForecastDay> Forecast { get; init; }
    }

    public class ForecastDay
    {
        [JsonPropertyName("day")]
        public string Day { get; set; }

        [JsonPropertyName("temperature")]
        public string Temperature { get; set; }

        [JsonPropertyName("wind")]
        public string Wind { get; set; }
    }

    
    public override async Task<string> Get() {
        string location = LocationReader.GetRandomLocationFromFile();
        string response = await APIClient.Instance.Fetch(url+location, Parameters);
        WeatherForecast forecast = JsonSerializer.Deserialize<WeatherForecast>(response)!;
        return "Weather in " + location + " is " + forecast.Forecast[0].Temperature;
    }
}