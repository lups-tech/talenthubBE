using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models.Users
{
    public class AddUserToOrgRequest
    {
        [Required]
        public required string Email {get; set;}
        [Required]
        public required string Role {get; set;}
        [Required]
        public required string Name {get; set;}
    }
}