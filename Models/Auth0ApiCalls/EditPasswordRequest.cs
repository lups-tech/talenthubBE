using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace talenthubBE.Models.Auth0ApiCalls
{
    public class EditPasswordRequest
    {
       [Required]
       [JsonPropertyName("password")]
        public required string Password {get; set;}
    }
}