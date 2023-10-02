using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using talenthubBE.Models;
using talenthubBE.Models.Users;

namespace talenthubBE.Data.Repositories.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDTO>?> GetAllUsers();
        Task<UserDTO?> GetUser(String id);
        Task<UserDTO?> PutUser(String id, User User);
        Task<UserDTO?> PostUser(String userId, String orgId);
        Task DeleteUser(String id);
        Task<UserDTO?> AddUserDeveloper(UserDeveloperRequest request);
        Task<bool> DeleteUserDeveloper(UserDeveloperRequest request);
        Task<UserDTO?> AddUserJob(UserJobRequest request);
        Task<bool> DeleteUserJob(UserJobRequest request);
    }
}