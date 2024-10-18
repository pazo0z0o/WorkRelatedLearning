using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApp_UnderTheHood.DTO;
using static WebApp_UnderTheHood.Pages.Account.LoginModel;


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
            var response =await httpClient.PostAsJsonAsync("auth", new Credentials { UserName = "admin", Password = "password" });
            response.EnsureSuccessStatusCode();
            string strJwt = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<JwtToken>(strJwt);

            weatherForecastItems = await httpClient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast")??new(); //the endpoint of the controller
        
        
        
        
        }
    }
}

