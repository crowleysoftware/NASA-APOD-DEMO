using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace WeatherFunction
{
    public class NasaFunctions
    {
        private readonly ILogger<NasaFunctions> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public NasaFunctions(ILogger<NasaFunctions> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;

        }

        [Function("apod")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var apikey = Environment.GetEnvironmentVariable("NASA_API_KEY"); 

            var result = await _httpClientFactory.CreateClient().GetAsync($"https://api.nasa.gov/planetary/apod?api_key={apikey}");

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            // Deserialize the JSON response with System.Text.Json
            var apod = JsonSerializer.Deserialize<Apod>(content);

            return new OkObjectResult(apod);
        }
    }

    public class Apod
    {
        public string date { get; set; }
        public string explanation { get; set; }
        public string hdurl { get; set; }
        public string media_type { get; set; }
        public string service_version { get; set; }
        public string title { get; set; }
        public string url { get; set; }
    }
}
