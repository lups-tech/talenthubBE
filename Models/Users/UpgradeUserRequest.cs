using System.ComponentModel.DataAnnotations;

namespace talenthubBE.Models.Users
{
    public class UpgradeUserRequest
    {
        [Required]
        public required string UserId {get; set;} 
        [Required]
        public required string Role {get; set;}
    }
}