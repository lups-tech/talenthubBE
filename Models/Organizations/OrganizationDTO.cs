using talenthubBE.Models.Users;
using talenthubBE.Models.Developers;
using talenthubBE.Models.Jobs;

namespace talenthubBE.Models.Organizations
{
    public class OrganizationDTO
    {
        public required String Id { get; set; }
        public ICollection<UserDTO> Users { get; set; } = new List<UserDTO>();
        public ICollection<DeveloperDTO> Developers { get; set; } = new List<DeveloperDTO>();
        public ICollection<JobDTO> Jobs { get; set; } = new List<JobDTO>();
    }
}