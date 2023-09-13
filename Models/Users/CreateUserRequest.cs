using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace talenthubBE.Models.Users
{
    public class CreateUserRequest
    {
        public String Auth0Id { get; set; } = "";
        public String? Name {get; set;}
        public String? Email {get; set;}
        public String? Role {get; set;}
    }
}