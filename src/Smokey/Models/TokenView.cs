﻿using Newtonsoft.Json;

namespace Smokey.Models
{
    public sealed class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = "";

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; } = "";
    }
}
