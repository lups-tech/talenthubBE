using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using talenthubBE.Models;
using talenthubBE.Models.Developers;
using talenthubBE.Models.Jobs;
using talenthubBE.Models.Users;

namespace talenthubBE.Mapping
{
    public static class UserApiMapper
    {
        public static UserDTO ToUserDTO (this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                Jobs = user.Jobs.JobsMapper(),
                Developers = user.Developers.DevelopersMapper(),
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
    }
}