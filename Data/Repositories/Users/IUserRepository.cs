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
        Task<IEnumerable<UserDTO>?> GetAllUsers(string orgId);
        Task<UserDTO?> GetUser(string id);
        Task<IEnumerable<Auth0User>?> GetAuth0Sales(string orgId);
        Task<IEnumerable<Auth0User>?> GetAuth0Admins(string orgId);
        Task<UserDTO?> PutUser(string id, User User);
        Task<bool> RegisterUserWithAuth0(string orgId, string email, string role, string name);
        Task UpgradeUser(string userId, string orgId, string role);
        Task EditUser(string userId, EditUserRequest request);
        Task EditPassword(string userId, EditPasswordRequest request);
        Task<UserDTO?> PostUser(string userId, string orgId);
        Task DeleteUser(string id);
        Task<UserDTO?> AddUserDeveloper(UserDeveloperRequest request);
        Task<bool> DeleteUserDeveloper(UserDeveloperRequest request);
        Task<UserDTO?> AddUserJob(UserJobRequest request);
        Task<bool> DeleteUserJob(UserJobRequest request);
    }
}