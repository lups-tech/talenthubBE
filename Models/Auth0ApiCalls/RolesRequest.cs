using System.Text.Json.Serialization;

namespace talenthubBE.Models
{
    public class RolesRequest
    {
        [JsonPropertyName("roles")]
        public required String[] Roles {get; set;}
    }
}