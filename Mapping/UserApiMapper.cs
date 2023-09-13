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
        public static UserDTO ToDevDTO (this User User)
        {
            return new UserDTO
            {
                Id = User.Id,
                Auth0Id = User.Auth0Id,
                Name = User.Name,
                Email = User.Email,
                Role = User.Role,
                Jobs = User.Jobs.JobsMapper(),
                Developers = User.Developers.DevelopersMapper(),
            };
        }
        public static User ToUser(this CreateUserRequest newUser)
        {
            return new User
            {
                Id = new Guid(),
                Auth0Id = newUser.Auth0Id,
                Name = newUser.Name,
                Email = newUser.Email,
                Role = newUser.Role,
                CreatedAt = DateTime.Now.ToUniversalTime(),
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