using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using talenthubBE.Data;
using talenthubBE.Helpers;
using talenthubBE.Models.Developers;

namespace talenthubBE.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly IDevelopersRepository _repository;

        public DevelopersController(IDevelopersRepository developersRepository)
        {
            _repository = developersRepository;
        }

        // GET: api/Developers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeveloperDTO>>> GetDevelopers()
        {
          IEnumerable<DeveloperDTO>? response = await _repository.GetAllDevelopers();
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // GET: api/Developers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeveloperDTO>> GetDeveloper(Guid id)
        {
            DeveloperDTO? response = await _repository.GetDeveloper(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // PUT: api/Developers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<DeveloperDTO>> PutDeveloper(Guid id, Developer developer)
        {
            if (id != developer.Id)
            {
                return BadRequest();
            }
            DeveloperDTO? response = await _repository.PutDeveloper(id, developer);
            if(response == null)
            {
                return NotFound(new {message = "No such developer found"});
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

        // POST: api/Developers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeveloperDTO>> PostDeveloper(CreateDeveloperRequest request)
        {
            string userId = ControllerHelper.UserIdFinder(User);
            string orgId = ControllerHelper.OrgIdFinder(User);

            DeveloperDTO? response = await _repository.PostDeveloper(userId, orgId, request);
            if(response == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetDeveloper", new { id = response.Id }, response);
        }

        [HttpPatch("/api/developerSkills")]
        public async Task<ActionResult<DeveloperDTO>> AddDeveloperSkills(CreateDeveloperSkillsRequest request)
        {
            DeveloperDTO response = await _repository.AddDeveloperSkills(request);
            return CreatedAtAction("GetDeveloper", new { id = response.Id }, response);
        }

        // DELETE: api/Developers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeveloper(Guid id)
        {
            try
            {
                await _repository.DeleteDeveloper(id);
            }
            catch (Exception e)
            {
                return NotFound(new {message = e.Message});
            }
            return NoContent();
        }

        [HttpDelete("/api/developerskills")]
        public async Task<IActionResult> DeleteDeveloperSkills(DeleteDeveloperSkillsRequest request)
        {
            if(await _repository.DeleteDeveloperSkill(request))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpGet("/api/testscope")]
        [Authorize("create:admin")]
        public IActionResult Test()
        {
            return Ok("It Worked!");
        }
    }
}
