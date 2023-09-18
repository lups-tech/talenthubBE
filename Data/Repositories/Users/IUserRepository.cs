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
        Task<UserDTO?> GetUser(Guid id);
        Task<UserDTO?> PutUser(Guid id, User User);
        Task<UserDTO?> PostUser(CreateUserRequest request);
        Task DeleteUser(Guid id);
    }
}