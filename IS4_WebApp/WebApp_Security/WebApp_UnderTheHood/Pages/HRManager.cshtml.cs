using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp_UnderTheHood.DTO;

namespace WebApp_UnderTheHood.Pages
{
    [Authorize (Policy="HRManagerOnly")]
    public class HRManagerModel : PageModel
    {
        //injecting httpclient factory
        public IHttpClientFactory httpClientFactory;

        [BindProperty]
        public List<WeatherForecastDTO> weatherForecastItems { get; set; } = new();
        public HRManagerModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }


        public async Task OnGetAsync()
        {
            var httpClient =  httpClientFactory.CreateClient("OurWebAPI");
            weatherForecastItems = await httpClient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast")??new(); //the endpoint of the controller
        
        
        
        
        }
    }
}

