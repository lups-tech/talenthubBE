using talenthubBE.Models;
using talenthubBE.Models.Organizations;
using talenthubBE.Models.Developers;
using talenthubBE.Models.Jobs;
using talenthubBE.Models.Users;

namespace talenthubBE.Mapping
{
    public static class OrganizationApiMapper
    {
        public static OrganizationDTO ToOrganizationDTO (this Organization org)
        {
            return new OrganizationDTO
            {
                Id = org.Id,
                Jobs = org.Jobs.JobsMapper(),
                Developers = org.Developers.DevelopersMapper(),
                Users = org.Users.UsersMapper(),
            };
        }
        private static List<JobDTO> JobsMapper (this ICollection<Job> jobs)
        {
            List<JobDTO> output = new();
            foreach (Job job in jobs)
            {
                output.Add(job.ToJobDTO());
            }

            return output;
        }
        private static List<DeveloperDTO> DevelopersMapper (this ICollection<Developer> developers)
        {
            List<DeveloperDTO> output = new();
            foreach (Developer developer in developers)
            {
                output.Add(developer.ToDevDTO());
            }

            return output;
        }
        private static List<UserDTO> UsersMapper (this ICollection<User> users)
        {
            List<UserDTO> output = new();
            foreach (User user in users)
            {
                output.Add(user.ToUserDTO());
            }

            return output;
        }
    }
}