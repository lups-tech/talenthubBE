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
        [HttpPatch("interviews/{processId}")]
        public async Task<ActionResult<MatchingProcessDTO>> PatchInterview(Guid processId, InterviewDataDTO request)
        {
            try
            {
                MatchingProcessDTO? response = await _repository.PatchInterview(processId, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPatch("contracts/{processId}")]
        public async Task<ActionResult<MatchingProcessDTO>> PatchContract(Guid processId, ContractDataDTO request)
        {
            try
            {
                MatchingProcessDTO? response = await _repository.PatchContract(processId, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPatch("proposals/{processId}")]
        public async Task<ActionResult<MatchingProcessDTO>> PatchProposals(Guid processId, ProposedDataDTO request)
        {
            try
            {
                MatchingProcessDTO? response = await _repository.PatchProposed(processId, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
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
        [HttpDelete("proposals/{id}")]
        public async Task<IActionResult> DeleteProposal(Guid id)
        {
            try
            {
                await _repository.DeleteProposal(id);
            }
            catch (Exception e)
            {
                return NotFound(new {message = e.Message});
            }
            return NoContent();
        }
        [HttpDelete("interviews/{id}")]
        public async Task<IActionResult> DeleteInterviews(Guid id)
        {
            try
            {
                await _repository.DeleteInterview(id);
            }
            catch (Exception e)
            {
                return NotFound(new {message = e.Message});
            }
            return NoContent();
        }
        [HttpDelete("contracts/{id}")]
        public async Task<IActionResult> DeleteContract(Guid id)
        {
            try
            {
                await _repository.DeleteContract(id);
            }
            catch (Exception e)
            {
                return NotFound(new {message = e.Message});
            }
            return NoContent();
        }
    }
}
