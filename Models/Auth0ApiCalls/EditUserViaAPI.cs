using System.Text.Json.Serialization;

namespace talenthubBE.Models.Auth0ApiCalls
{
    public class EditUserViaAPI
    {
        [JsonPropertyName("name")]
        public required string Name {get; set;}
        [JsonPropertyName("nickname")]
        public required string Nickname {get; set;}
        [JsonPropertyName("password")]
        public required string Password {get; set;}
        [JsonPropertyName("username")]
        public required string Username {get; set;}
    }
}