using System.Net.Http.Json;

namespace ClientApp.Components;

public class SunService
{
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task<(DateTime sunrise, DateTime sunset)> GetSunriseSunsetAsync(double latitude, double longitude, DateTime date)
    {
        var url = $"https://api.sunrise-sunset.org/json?lat={latitude}&lng={longitude}&date={date:yyyy-MM-dd}&formatted=0";

        var result = await _httpClient.GetFromJsonAsync<SunriseSunsetResponse>(url);
        //var response = await _httpClient.GetStringAsync(url);
        //var result = JsonConvert.DeserializeObject<SunriseSunsetResponse>(response);

        if (result.status != "OK")
        {
            throw new Exception("Error fetching sunrise and sunset data");
        }

        DateTime sunriseTime = DateTime.Parse(result.results.sunrise);
        DateTime sunsetTime = DateTime.Parse(result.results.sunset);

        return (sunriseTime, sunsetTime);
    }
    
    class SunriseSunsetResponse
    {
        public Results results { get; set; }
        public string status { get; set; }
    }
    
    class Results
    {
        public string sunrise { get; set; }
        public string sunset { get; set; }
        // Der kan være flere felter, men disse er de vigtigste
    }
}