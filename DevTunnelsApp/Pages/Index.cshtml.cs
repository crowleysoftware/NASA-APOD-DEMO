using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

namespace DevTunnelsApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient httpClient;
        public Apod? Apod { get; set; }

        public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient)
        {
            _logger = logger;
            this.httpClient = httpClient;
        }

        public async Task OnGet()
        {
            var apikey = Environment.GetEnvironmentVariable("NASA_API_KEY");
            var result = await httpClient.GetAsync($"https://api.nasa.gov/planetary/apod?api_key={apikey}");
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            Apod = JsonSerializer.Deserialize<Apod>(content);
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
