using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
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
        [OpenApiOperation(operationId: "getApod", tags: ["APOD"], Summary = "Get Astronomy Picture of the Day", Description = "Returns the NASA Astronomy Picture of the Day data including title, explanation, date, and image URL.")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Apod), Summary = "Successful response", Description = "Returns the APOD object with date, title, explanation, and image URLs.")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            Apod apod = new Apod
            {
                date = "2026-03-14",
                explanation = "This response is coming from my laptop",
                hdurl = "https://fastly.picsum.photos/id/184/4288/2848.jpg?hmac=l0fKWzmWf6ISTPMEm1WjRdxn35sg6U3GwZLn5lvKhTI",
                media_type = "image",
                service_version = "v1",
                title = "Dev Tunnels in action!",
                url = "https://fastly.picsum.photos/id/184/4288/2848.jpg?hmac=l0fKWzmWf6ISTPMEm1WjRdxn35sg6U3GwZLn5lvKhTI"
            };

            return new OkObjectResult(apod);

            //https://fastly.picsum.photos/id/184/4288/2848.jpg?hmac=l0fKWzmWf6ISTPMEm1WjRdxn35sg6U3GwZLn5lvKhTI
            //_logger.LogInformation("C# HTTP trigger function processed a request.");

            //var apikey = Environment.GetEnvironmentVariable("NASA_API_KEY"); 

            //var result = await _httpClientFactory.CreateClient().GetAsync($"https://api.nasa.gov/planetary/apod?api_key={apikey}");

            //result.EnsureSuccessStatusCode();

            //var content = await result.Content.ReadAsStringAsync();

            //// Deserialize the JSON response with System.Text.Json
            //var apod = JsonSerializer.Deserialize<Apod>(content);

            //return new OkObjectResult(apod);
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
