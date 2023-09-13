using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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

        public async Task<UserDTO?> GetUser(Guid id)
        {
            if (_context.Users == null)
            {
                return null;
            }
            var user = await _context.Users.Include("Developers").Include("Jobs").FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;
            }

            return user.ToUserDTO();
        }

        public async Task<UserDTO?> PostUser(CreateUserRequest request)
        {
            if (_context.Users == null)
            {
                return null;
            }
            User newUser = request.ToUser();
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser.ToUserDTO();
        }

        public async Task<UserDTO?> PutUser(Guid id, User user)
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

        public async Task DeleteUser(Guid id)
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

        private bool UserExists(Guid id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}