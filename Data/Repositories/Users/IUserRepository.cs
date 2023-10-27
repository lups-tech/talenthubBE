using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using talenthubBE.Models;
using talenthubBE.Models.Auth0ApiCalls;
using talenthubBE.Models.Users;

namespace talenthubBE.Data.Repositories.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDTO>?> GetAllUsers(String orgId);
        Task<UserDTO?> GetUser(String id);
        Task<IEnumerable<Auth0User>?> GetAuth0Users(String orgId);
        Task<UserDTO?> PutUser(String id, User User);
        Task<bool> RegisterUserWithAuth0(String orgId, String email, String role, String name);
        Task UpgradeUser(String userId, String role);
        Task EditUser(String userId, EditUserRequest request);
        Task EditPassword(String userId, EditPasswordRequest request);
        Task<UserDTO?> PostUser(String userId, String orgId);
        Task DeleteUser(String id);
        Task<UserDTO?> AddUserDeveloper(UserDeveloperRequest request);
        Task<bool> DeleteUserDeveloper(UserDeveloperRequest request);
        Task<UserDTO?> AddUserJob(UserJobRequest request);
        Task<bool> DeleteUserJob(UserJobRequest request);
    }
}