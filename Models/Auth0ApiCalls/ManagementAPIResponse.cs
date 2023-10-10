using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace talenthubBE.Models
{
    public class ManagementAPIResponse
    {
        [JsonPropertyName("access_token")]
        public String? AccessToken {get; set;}
        [JsonPropertyName("scope")]
        public String? Scope {get; set;}
        [JsonPropertyName("expires_in")]
        public int ExpiresIn {get; set;}
        [JsonPropertyName("token_type")]
        public String? TokenType {get; set;}
    }
}