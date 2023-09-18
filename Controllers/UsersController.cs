using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<User>> GetUser(Guid id)
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
        public async Task<IActionResult> PutUser(Guid id, User user)
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
        public async Task<ActionResult<User>> PostUser(CreateUserRequest request)
        {
            UserDTO? response = await _repository.PostUser(request);
            if(response == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetDeveloper", new { id = response.Id }, response);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
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
    }
}
