﻿using Newtonsoft.Json;

namespace WebApp_UnderTheHood.Authorization
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = String.Empty;
        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }
    }
}
