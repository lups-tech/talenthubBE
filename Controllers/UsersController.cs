using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using talenthubBE.Data.Repositories.Users;
using talenthubBE.Helpers;
using talenthubBE.Models;
using talenthubBE.Models.Auth0ApiCalls;
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
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            string orgId = ControllerHelper.OrgIdFinder(User);
            IEnumerable<UserDTO>? response = await _repository.GetAllUsers(orgId);
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("/api/auth0sales")]
        [Authorize("create:users")]
        public async Task<ActionResult<IEnumerable<Auth0User>>> GetAuth0Sales()
        {
            string orgId = ControllerHelper.OrgIdFinder(User);
            IEnumerable<Auth0User>? response = await _repository.GetAuth0Sales(orgId);
            return Ok(response);
        }
        [HttpGet("/api/auth0admin")]
        [Authorize("create:users")]
        public async Task<ActionResult<IEnumerable<Auth0User>>> GetAuth0Admins()
        {
            string orgId = ControllerHelper.OrgIdFinder(User);
            IEnumerable<Auth0User>? response = await _repository.GetAuth0Admins(orgId);
            return Ok(response);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(String id)
        {
            String currentUser = ControllerHelper.UserIdFinder(User);
            UserDTO? response = id == "self" 
                ? await _repository.GetUser(currentUser)
                : await _repository.GetUser(id);
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
            if (id != user.Id)
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
        public async Task<ActionResult<UserDTO>> PostUser() 
        {
            string userId = ControllerHelper.UserIdFinder(User);
            string orgId = ControllerHelper.OrgIdFinder(User);

            UserDTO? response = await _repository.PostUser(userId, orgId);
            try
            {
                if(response == null)
                {
                    return Ok();
                }
                return CreatedAtAction("GetUser", new { id = response.Id }, response);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("/api/users/register")]
        [Authorize("create:users")]
        public async Task<ActionResult> PostUserEmail(AddUserToOrgRequest request) 
        {
            string orgId = ControllerHelper.OrgIdFinder(User);
            bool response = await _repository.RegisterUserWithAuth0(orgId, request.Email, request.Role, request.Name);
            if (!response)
            {
                return BadRequest();
            }
            return Ok();   
        }

        [HttpPatch("/api/users/edit")]
        public async Task<ActionResult> EditUserDetails(EditUserRequest request) 
        {
            string userId = ControllerHelper.UserIdFinder(User);
            await _repository.EditUser(userId, request);
            return Ok();   
        }

        [HttpPatch("/api/users/editpassword")]
        public async Task<ActionResult> EditUserPassword(EditPasswordRequest request) 
        {
            string userId = ControllerHelper.UserIdFinder(User);
            await _repository.EditPassword(userId, request);
            return Ok();   
        }

        [HttpPatch("/api/users/upgrade")]
        [Authorize("create:admin")]
        public async Task<ActionResult> PatchUserToAdmin(UpgradeUserRequest request)
        {
            string orgId = ControllerHelper.OrgIdFinder(User);
            try
            {
                await _repository.UpgradeUser(request.UserId, orgId, request.Role);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize("create:users")]
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
            UserDTO? response = await _repository.AddUserDeveloper(request);
            if(response == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetUser", new { id = response.Id }, response);
        }
        [HttpPatch("/api/userjob")]
        public async Task<ActionResult<UserDTO>> AddUserJob(UserJobRequest request)
        {

            UserDTO? response = await _repository.AddUserJob(request);
            if(response == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetUser", new { id = response.Id }, response);
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
