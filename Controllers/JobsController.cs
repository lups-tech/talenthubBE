using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using talenthubBE.Data;
using talenthubBE.Helpers;
using talenthubBE.Models.Jobs;

namespace talenthubBE.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobsRepository _repository;

        public JobsController(IJobsRepository jobsRepository)
        {
            _repository = jobsRepository;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDTO>>> GetJobs()
        {
          String orgId = ControllerHelper.OrgIdFinder(User);
          IEnumerable<JobDTO>? response = await _repository.GetAllJobs(orgId);
          if(response == null)
          {
            return NotFound();
          }
            return Ok(response);
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDTO>> GetJob(Guid id)
        {
            String orgId = ControllerHelper.OrgIdFinder(User);
            JobDTO? response = await _repository.GetJob(id, orgId);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // PUT: api/Jobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<JobDTO>> PutJob(Guid id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }
            try
            {
            String orgId = ControllerHelper.OrgIdFinder(User);
            JobDTO? response = await _repository.PutJob(id, job);
            if(response == null)
            {
                return NotFound(new {message = "No such job found"});
            }
                return Ok(response);
            }
            catch(Exception)
            {
                return Conflict(new {message = "There has been an issue handling your request"});
            }
        }

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobDTO>> PostJob(CreateJobRequest request)
        {
            String userId = ControllerHelper.UserIdFinder(User);
            String orgId = ControllerHelper.OrgIdFinder(User);
            
            JobDTO? response = await _repository.PostJob(userId, orgId, request);
            if(response == null)
            {
                return Conflict(new {message = "Job already saved"});
            }

            return CreatedAtAction("GetJob", new { id = response.Id }, response);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            try
            {
                await _repository.DeleteJob(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(new {message = e.Message});
            }
        }
    }
}
