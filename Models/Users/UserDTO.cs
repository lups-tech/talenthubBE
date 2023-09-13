using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using talenthubBE.Models.Developers;
using talenthubBE.Models.Jobs;

namespace talenthubBE.Models.Users
{
    public class UserDTO
    {
        public Guid Id {get; set;}
        public String Auth0Id { get; set; } = "";
        public String? Name {get; set;}
        public String? Email {get; set;}
        public String? Role {get; set;}
        public ICollection<JobDTO> Jobs { get; set; } = new List<JobDTO>();
        public ICollection<DeveloperDTO> Developers { get; set; } = new List<DeveloperDTO>();
    }
}