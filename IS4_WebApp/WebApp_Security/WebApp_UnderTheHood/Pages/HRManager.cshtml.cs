using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApp_UnderTheHood.Authorization;
using WebApp_UnderTheHood.DTO;
using static WebApp_UnderTheHood.Pages.LoginModel;


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
            //get token from session
            JwtToken token = new JwtToken();
            var strTokenObj = HttpContext.Session.GetString("access_token");
            if(string.IsNullOrEmpty(strTokenObj))
            {
              token = await Authenticate();
            }
            else
            {
                token = JsonConvert.DeserializeObject<JwtToken>(strTokenObj)?? new JwtToken();
            }

            if (token == null || string.IsNullOrWhiteSpace(token.AccessToken) || token.ExpiresAt <= DateTime.UtcNow) 
            {
                token = await Authenticate();
            }
            var httpClient = httpClientFactory.CreateClient("OurWebAPI");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken ?? String.Empty); 
            weatherForecastItems = await httpClient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast")??new(); //the endpoint of the controller
        }

        private async Task<JwtToken> Authenticate()
        { //authentication and getting the token
            var httpClient = httpClientFactory.CreateClient("OurWebAPI");
                var response = await httpClient.PostAsJsonAsync("auth", new Credentials { UserName = "admin", Password = "password" });
                response.EnsureSuccessStatusCode();
                string strJwt = await response.Content.ReadAsStringAsync();
            HttpContext.Session.SetString("access_token",strJwt);

            return JsonConvert.DeserializeObject<JwtToken>(strJwt) ?? new JwtToken();
        }
    }
}

