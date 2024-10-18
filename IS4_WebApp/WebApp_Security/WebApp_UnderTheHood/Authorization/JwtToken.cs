namespace WebApp_UnderTheHood.Authorization
{
    public class JwtToken
    {
        public string AccessToken { get; set; } = String.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
