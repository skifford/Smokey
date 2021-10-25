using Newtonsoft.Json;

namespace Smokey.Extensions.Models
{
    public class TokenView
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = "";

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; } = "";
    }
}
