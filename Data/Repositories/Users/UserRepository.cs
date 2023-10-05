using Microsoft.EntityFrameworkCore;
using talenthubBE.Mapping;
using talenthubBE.Models;
using talenthubBE.Models.Users;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NuGet.Packaging;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace talenthubBE.Data.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly MvcDataContext _context;
        private readonly IConfiguration _configuration;
        public UserRepository(MvcDataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
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
            var user = await _context.Users.Include("Developers").Include("Jobs").FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;
            }

            return user.ToUserDTO();
        }

        public async Task<UserDTO?> PostUser(String userId, String orgId)
        {
            if (_context.Users == null)
            {
                return null;
            }
            if(_context.Users.Any(u => u.Id == userId))
            {
                return null;
            }
            User newUser = new()
            {
                Id = userId,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                OrganizationId = orgId,
                Organization = _context.Organizations.Single(o => o.Id == orgId)
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser.ToUserDTO();
        }

        public async Task<bool> RegisterUserWithAuth0(String orgId, String email, String role, String name)
        {
            if(!Enum.IsDefined(typeof(Roles), role))
            {
                return false;
            }

            String roleId = "";
            if (role == "Admin")
            {
                roleId = "rol_xasNtUO2PeKyAGvZ";
            } 
            if (role == "Sales")
            {
                roleId = "rol_SwiRJAqI5EUeA5vh";
            }

            HttpClient client = new(); 

            Uri uri = new($"{_configuration["Auth0:Domain"]}api/v2/organizations/{orgId}/invitations");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {await GetManagementToken()}");
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");

            InviterClass inviter = new()
            { Name = name };
            InviteeClass invitee = new()
            { Email = email };
            String[] roles = {roleId};
            InvitationEmail invitationEmail = new()
            {
                Inviter = inviter,
                Invitee = invitee,
                ClientId = _configuration["Auth0:ClientId"]!,
                ConnectionId = _configuration["connectionId"]!,
                TtlSec = 0,
                Roles =  roles,
                SendInvitationEmail = true,
            };

            JsonContent content = JsonContent.Create<InvitationEmail>(invitationEmail);
            
            var response = await client.PostAsync(uri, content);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            return true;
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
                throw new Exception("User not found");
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
                .Single(u => u.Id == request.UserId);
            
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
            User selectedUser = await _context.Users
                .Include("Developers")
                .SingleAsync(u => u.Id == request.UserId);
            
            Developer developerToRemove = await _context.Developers
                .SingleAsync(d => d.Id == request.DeveloperId);
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
                .Single(u => u.Id == request.UserId);

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
                .Single(u => u.Id == request.UserId);

            Job jobToRemove = _context.JobDescriptions
                .Single(j => j.Id == request.JobId); 

            selectedUser.Jobs.Remove(jobToRemove);
            await _context.SaveChangesAsync();
            return true;
        }
        private bool UserExists(String id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool DeveloperExists(Guid id)
        {
            return (_context.Developers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool JobExists(Guid id)
        {
            return (_context.JobDescriptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private async Task<String> GetManagementToken()
        {
            HttpClient client = new();
            Uri uri = new($"{_configuration["Auth0:Domain"]}oauth/token");
            var content = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<String, String>("grant_type", "client_credentials"),
                new KeyValuePair<String, String>("client_id", _configuration["Auth0:ManagementClientId"]),
                new KeyValuePair<String, String>("client_secret", _configuration["Auth0:ClientSecret"]),
                new KeyValuePair<String, String>("audience", $"{_configuration["Auth0:Domain"]}api/v2/"),
            });
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            var response = await client.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            ManagementAPIResponse jsonString = await response.Content.ReadFromJsonAsync<ManagementAPIResponse>();
            return jsonString.AccessToken;
        }
    }
}