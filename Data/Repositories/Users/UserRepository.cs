using Microsoft.EntityFrameworkCore;
using talenthubBE.Mapping;
using talenthubBE.Models;
using talenthubBE.Models.Users;

namespace talenthubBE.Data.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly MvcDataContext _context;
        public UserRepository(MvcDataContext context) => _context = context;

        public async Task<IEnumerable<UserDTO>?> GetAllUsers()
        {
            if (_context.Users == null)
            {
                return null;
            }
            var res = await _context.Users.Include("Developers").Include("Jobs").ToListAsync();

            List<UserDTO> users = new();
            foreach (User user in res)
            {
                users.Add(user.ToUserDTO());
            }

            return users;
        }

        public async Task<UserDTO?> GetUser(String id)
        {
            if (_context.Users == null)
            {
                return null;
            }
            var user = await _context.Users.Include("Developers").Include("Jobs").FirstOrDefaultAsync(u => u.Auth0Id == id);
            if (user == null)
            {
                return null;
            }

            return user.ToUserDTO();
        }

        public async Task<UserDTO?> PostUser(String userId)
        {
            if (_context.Users == null)
            {
                return null;
            }
            User newUser = new()
            {
                Auth0Id = userId, 
                CreatedAt = DateTime.Now.ToUniversalTime(),
            };
            if(_context.Users.Any(u => u.Auth0Id == userId))
            {
                return null;
            }
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser.ToUserDTO();
        }

        public async Task<UserDTO?> PutUser(String id, User user)
        {
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return user.ToUserDTO();
        }

        public async Task DeleteUser(String id)
        {
            if (_context.Users == null)
            {
                throw new Exception("context not found");
            }
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return;
        }

        public async Task<UserDTO?> AddUserDeveloper(UserDeveloperRequest request)
        {
            if(!UserExists(request.UserId) || !DeveloperExists(request.DeveloperId))
            {
                return null;
            }
            User selectedUser = _context.Users
                .Include("Developers")
                .Single(u => u.Auth0Id == request.UserId);
            
            Developer developerToAdd = _context.Developers
                .Single(d => d.Id == request.DeveloperId);

            selectedUser.Developers.Add(developerToAdd);
            await _context.SaveChangesAsync();
            
            return selectedUser.ToUserDTO();
        }

        public async Task<bool> DeleteUserDeveloper(UserDeveloperRequest request)
        {
            if (!UserExists(request.UserId))
            {
                return false;
            }
            User selectedUser = _context.Users
                .Include("Developers")
                .Single(u => u.Auth0Id == request.UserId);
            
            Developer developerToRemove = _context.Developers
                .Single(d => d.Id == request.DeveloperId);
            if (developerToRemove == null)
            {
                return false;
            }
        
            selectedUser.Developers.Remove(developerToRemove);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<UserDTO?> AddUserJob(UserJobRequest request)
        {
            if(!UserExists(request.UserId) || !JobExists(request.JobId))
            {
                return null;
            }
            User selectedUser = _context.Users
                .Include("Jobs")
                .Single(u => u.Auth0Id == request.UserId);

            Job jobToAdd = _context.JobDescriptions
                .Single(j => j.Id == request.JobId); 
            
            selectedUser.Jobs.Add(jobToAdd);
            await _context.SaveChangesAsync();
            return selectedUser.ToUserDTO();
        }

        public async Task<bool> DeleteUserJob(UserJobRequest request)
        {
            if(!UserExists(request.UserId) || !JobExists(request.JobId))
            {
                return false;
            }
            User selectedUser = _context.Users
                .Include("Jobs")
                .Single(u => u.Auth0Id == request.UserId);

            Job jobToRemove = _context.JobDescriptions
                .Single(j => j.Id == request.JobId); 

            selectedUser.Jobs.Remove(jobToRemove);
            await _context.SaveChangesAsync();
            return true;
        }
        private bool UserExists(String id)
        {
            return (_context.Users?.Any(e => e.Auth0Id == id)).GetValueOrDefault();
        }
        private bool DeveloperExists(Guid id)
        {
            return (_context.Developers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool JobExists(Guid id)
        {
            return (_context.JobDescriptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}