using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using talenthubBE.Data.Repositories.Users;
using talenthubBE.Models;
using talenthubBE.Models.Users;

namespace talenthubBE.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            IEnumerable<UserDTO>? response = await _repository.GetAllUsers();
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(String id)
        {
           UserDTO? response = await _repository.GetUser(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(String id, User user)
        {
            if (id != user.Auth0Id)
            {
                return BadRequest();
            }
            UserDTO? response = await _repository.PutUser(id, user);
            if(response == null)
            {
                return NotFound(new {message = "No such user found"});
            }
            try
            {
                return Ok(response);
            }
            catch(Exception)
            {
                return Conflict(new {message = "There has been an issue handling your request"});
            }
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser()
        {
            String authId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            UserDTO? response = await _repository.PostUser(authId);
            if(response == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetUser", new { id = response.Auth0Id }, response);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(String id)
        {
            try
            {
                await _repository.DeleteUser(id);
            }
            catch (Exception e)
            {
                return NotFound(new {message = e.Message});
            }
            return NoContent();
        }
        [HttpPatch("/api/userdeveloper")]
        public async Task<ActionResult<UserDTO>> AddUserDeveloper(UserDeveloperRequest request)
        {
            String authId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            UserDTO? response = await _repository.AddUserDeveloper(request);
            if(response == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetUser", new { id = response.Auth0Id }, response);
        }
        [HttpPatch("/api/userjob")]
        public async Task<ActionResult<UserDTO>> AddUserJob(UserJobRequest request)
        {
            String authId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            UserDTO? response = await _repository.AddUserJob(request);
            if(response == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetUser", new { id = response.Auth0Id }, response);
        }
        [HttpDelete("/api/userdeveloper")]
        public async Task<IActionResult> DeleteUserDeveloper(UserDeveloperRequest request)
        {
            String authId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if(await _repository.DeleteUserDeveloper(request))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("/api/userjob")]
        public async Task<IActionResult> DeleteUserJob(UserJobRequest request)
        {
            String authId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if(await _repository.DeleteUserJob(request))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
