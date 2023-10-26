using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace talenthubBE.Models.Auth0ApiCalls
{
    public class EditUserRequest

    {
       [Required]
       [JsonPropertyName("name")]
        public required string Name {get; set;}
       [Required]
       [JsonPropertyName("email")]
        public required string Email {get; set;}
    }
}