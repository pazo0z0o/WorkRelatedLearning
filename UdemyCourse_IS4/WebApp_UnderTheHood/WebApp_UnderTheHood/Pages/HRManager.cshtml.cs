using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApp_UnderTheHood.Authorization;
using WebApp_UnderTheHood.DTO;
using static WebApp_UnderTheHood.Authorization.Credential;

namespace WebApp_UnderTheHood.Pages
{
    [Authorize(Policy = "HRManagerOnly")]
    public class HRManagerModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public List<WeatherForecastDTO> WeatherForecastItems { get; set; } //= new List<WeatherForecastDTO>();

        public HRManagerModel(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            //get token from session 
            JwtToken token = null;
            //for authentication and getting the token
            try
            {
                var strTokenObj = HttpContext.Session.GetString("access_token");
                if (string.IsNullOrEmpty(strTokenObj))
                {
                    var httpClient = _httpClientFactory.CreateClient("OurWebAPI");
                    var res = await httpClient.PostAsJsonAsync("auth", new Credential { UserName = "admin", Password = "pass" });
                    res.EnsureSuccessStatusCode(); //good practice
                                                   //read and deserialize token from a string
                    string strJWT = await res.Content.ReadAsStringAsync();


                }
                else
                {
                    token = JsonConvert.DeserializeObject<JwtToken>(strTokenObj);
                }
                if(token == null || string.IsNullOrWhiteSpace(token.AccessToken) || token.ExpiresAt <= DateTime.UtcNow)
                { 
                
                
                
                }
                     
                



               // httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
               //     "Bearer", token.AccessToken);

                //WeatherForecastItems = await httpClient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
