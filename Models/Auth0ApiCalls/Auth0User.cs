using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace talenthubBE.Models.Auth0ApiCalls
{
    public class Auth0User
    {
        [JsonProperty("user_id")]
        public required string Auth0Id {get; set;}
        [JsonProperty("email")]
        public required string Email {get; set;}
        [JsonProperty("name")]
        public required string Name {get; set;}
        [JsonProperty("picture")]
        public required string Picture {get; set;}
    }
}