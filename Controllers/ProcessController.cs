using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using talenthubBE.Data;
using talenthubBE.Data.Repositories.Process;
using talenthubBE.Helpers;
using talenthubBE.Models;

namespace talenthubBE.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MatchingProcessController : ControllerBase
    {
        private readonly IMatchingProcessRepository _repository;

        public MatchingProcessController(IMatchingProcessRepository MatchingProcessRepository)
        {
            _repository = MatchingProcessRepository;
        }

        // GET: api/MatchingProcess
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchingProcessDTO>>> GetMatchingProcess()
        {
            string userId = ControllerHelper.UserIdFinder(User);
            IEnumerable<MatchingProcessDTO>? response = await _repository.GetAllProcesses(userId);
                if(response == null)
                {
                    return NotFound();
                }
                return Ok(response);
        }

        // GET: api/MatchingProcess/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchingProcessDTO>> GetMatchingProcess(Guid id)
        {
            string orgId = ControllerHelper.OrgIdFinder(User);
            MatchingProcessDTO? response = await _repository.GetProcess(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }   

        // POST: api/MatchingProcess
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MatchingProcessDTO>> PostMatchingProcess(CreateProcessRequest request)
        {
            string userId = ControllerHelper.UserIdFinder(User);

            MatchingProcessDTO? response = await _repository.PostProcess(userId, request);
            if(response == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetMatchingProcess", new { id = response.Id }, response);
        }

        [HttpPatch]
        public async Task<ActionResult<MatchingProcessDTO>> PatchMatchingProcess(EditProcessRequest request)
        {
            MatchingProcessDTO? response = await _repository.PatchProcess(request);
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPatch("/api/deleteentries")]
        public async Task<ActionResult<MatchingProcessDTO>> EditMatchingProcess(EditProcessRequest request)
        {
            MatchingProcessDTO? response = await _repository.EditProcess(request);
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // DELETE: api/MatchingProcess/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchingProcess(Guid id)
        {
            try
            {
                await _repository.DeleteProcess(id);
            }
            catch (Exception e)
            {
                return NotFound(new {message = e.Message});
            }
            return NoContent();
        }
    }
}
