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
        public required String Id { get; set; }
        public required bool IsAdmin { get; set; }
        public ICollection<JobDTO> Jobs { get; set; } = new List<JobDTO>();
        public ICollection<DeveloperDTO> Developers { get; set; } = new List<DeveloperDTO>();
    }
}