using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using talenthubBE.Mapping;
using talenthubBE.Models.Jobs;

namespace talenthubBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly MvcDataContext _context;

        public JobsController(MvcDataContext context)
        {
            _context = context;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDTO>>> GetJob()
        {
          if (_context.JobDescriptions == null)
          {
              return NotFound();
          }
            var res = await _context.JobDescriptions.Include("Skills").ToListAsync();
        
            List<JobDTO> jobs = new();
            foreach (Job job in res)
            {
                jobs.Add(job.ToJobDTO());
            }

            return Ok(jobs);
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDTO>> GetJob(Guid id)
        {
          if (_context.JobDescriptions == null)
          {
              return NotFound();
          }
            var job = await _context.JobDescriptions.Include("Skills").FirstOrDefaultAsync(j => j.Id == id);

            if (job == null)
            {
                return NotFound();
            }

            return Ok(job.ToJobDTO());
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

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Job? newJob = await _context.JobDescriptions.Include("Skills").FirstOrDefaultAsync(j => j.Id == id);

            return Ok(newJob!.ToJobDTO());
        }

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobDTO>> PostJob(CreateJobRequest request)
        {
          if (_context.JobDescriptions == null)
          {
              return Problem("Entity set 'MvcDeveloperContext.Job'  is null.");
          }

            Job job = request.ToJob();
            _context.JobDescriptions.Add(job);
            var skillsToAdd = new List<Skill>();
            foreach (Guid skillId in request.SelectedSkillIds)
            {
                var currentSkill = _context.Skills
                    .Single(skill => skill.Id == skillId);
                skillsToAdd.Add(currentSkill);
            }

            job.Skills.AddRange(skillsToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = job.Id }, job.ToJobDTO());
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            if (_context.JobDescriptions == null)
            {
                return NotFound();
            }
            var job = await _context.JobDescriptions.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.JobDescriptions.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobExists(Guid id)
        {
            return (_context.JobDescriptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
