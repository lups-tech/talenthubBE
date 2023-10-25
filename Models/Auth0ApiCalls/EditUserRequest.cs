using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace talenthubBE.Models.Auth0ApiCalls
{
    public class EditUserRequest

    {
       [Required]
        public required string Name {get; set;}
       [Required]
        public required string Nickname {get; set;}
    
    }
}