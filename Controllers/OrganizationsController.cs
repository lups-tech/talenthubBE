using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using talenthubBE.Data.Repositories.Organizations;
using talenthubBE.Models;
using talenthubBE.Models.Organizations;

namespace talenthubBE.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationRepository _repository;

        public OrganizationsController(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Organizations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations()
        {
            IEnumerable<OrganizationDTO>? response = await _repository.GetAllOrganizations();
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // GET: api/Organizations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(String id)
        {
           OrganizationDTO? response = await _repository.GetOrganization(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // PUT: api/Organizations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganization(String id, Organization organization)
        {
            if (id != organization.Id)
            {
                return BadRequest();
            }
            OrganizationDTO? response = await _repository.PutOrganization(id, organization);
            if(response == null)
            {
                return NotFound(new {message = "No such organization found"});
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

        // POST: api/Organizations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Organization>> PostOrganization(String orgId) // Change orgId when we'll have the schema from the FE/Auth0 !!!!!!!!
        {
            OrganizationDTO? response = await _repository.PostOrganization(orgId);
            if(response == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetOrganization", new { id = response.Id }, response);
        }

        // DELETE: api/Organizations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(String id)
        {
            try
            {
                await _repository.DeleteOrganization(id);
            }
            catch (Exception e)
            {
                return NotFound(new {message = e.Message});
            }
            return NoContent();
        }
    }
}
