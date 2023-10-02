using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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
        public async Task<ActionResult<Organization>> PostOrganization(String orgId)
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
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(new {message = e.Message});
            }
        }

        [HttpPatch("api/organizationsuser")]
        public async Task<ActionResult<OrganizationDTO>> AddUserToOrganization(String orgId, String userId)
        {
            try
            {
                OrganizationDTO? response = await _repository.AddUserToOrganization(orgId, userId);
                if(response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, 500);
            }
        }  

        [HttpDelete("api/organizationsuser")]
        public async Task<IActionResult> RemoveUserFromOrganization(String orgId, String userId)
        {
            try
            {
                await _repository.RemoveUserFromOrganization(orgId, userId);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(new {message = e.Message});
            }    
        }

        [HttpPatch("api/organizationsjob")]
        public async Task<ActionResult<OrganizationDTO>> AddJobToOrganization(String orgId, Guid jobId)
        {
            try
            {
                OrganizationDTO? response = await _repository.AddJobToOrganization(orgId, jobId);
                if(response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, 500);
            }
        }

        [HttpDelete("api/organizationsjob")]
        public async Task<IActionResult> RemoveJobFromOrganization(String orgId, Guid jobId)
        {
            try
            {
                await _repository.RemoveJobFromOrganization(orgId, jobId);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(new {message = e.Message});
            }
        }

        [HttpPatch("api/organiztionsdeveloper")]

        public async Task<ActionResult<OrganizationDTO>> AddDeveloperToOrganization(String orgId, Guid userId)
        {
            try
            {
                OrganizationDTO? response = await _repository.AddDeveloperToOrganization(orgId, userId);
                if(response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, 500);
            }
        }

        [HttpDelete("api/organizationsdeveloper")]

        public async Task<IActionResult> RemoveDeveloperFromOrganization(String orgId, Guid devId)
        {
            try
            {
                await _repository.RemoveDeveloperFromOrganization(orgId, devId);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(new {message = e.Message});
            }
        }

    }
}
