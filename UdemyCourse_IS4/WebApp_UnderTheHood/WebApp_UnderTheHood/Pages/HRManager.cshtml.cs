using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp_UnderTheHood.DTO;

namespace WebApp_UnderTheHood.Pages
{
    [Authorize(Policy = "HRManagerOnly")]
    public class HRManagerModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public List<WeatherForecastDTO> WeatherForecastItems { get; set; }

        public HRManagerModel(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task OnGetAsync()
        {
           var httpClient = _httpClientFactory.CreateClient("OurWebAPI");
          WeatherForecastItems = await httpClient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast");
        }
    }
}
