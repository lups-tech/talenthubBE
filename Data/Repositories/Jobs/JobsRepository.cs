using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using talenthubBE.Mapping;
using talenthubBE.Models.Jobs;

namespace talenthubBE.Data
{
    public class JobsRepository : IJobsRepository
    {
        private readonly MvcDataContext _context;
        public JobsRepository(MvcDataContext context) => _context = context;
        public async Task<IEnumerable<JobDTO>?> GetAllJobs()
        {
            if (_context.JobDescriptions == null)
            {
                return null;
            }
            
            var res = await _context.JobDescriptions.Include("Skills").ToListAsync();
        
            List<JobDTO> jobs = new();
            foreach (Job job in res)
            {
                jobs.Add(job.ToJobDTO());
            }

            return jobs;
        }
        public async Task<JobDTO?> GetJob(Guid id)
        {
            if (_context.JobDescriptions == null)
            {
                return null;
            }

            var job = await _context.JobDescriptions.Include("Skills").FirstOrDefaultAsync(j => j.Id == id);
            if (job == null)
            {
                return null;
            }
            return job.ToJobDTO();
        }
        public async Task<JobDTO?> PutJob(Guid id, Job job)
        {
            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            Job? newJob = await _context.JobDescriptions.Include("Skills").FirstOrDefaultAsync(j => j.Id == id);

            return newJob!.ToJobDTO();
        }
        public async Task<JobDTO?> PostJob(CreateJobRequest request)
        {
            if (_context.JobDescriptions.Any(j => j.JobTechId == request.JobTechId))
            {
                return null;
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

            return job.ToJobDTO();
        }
        public async Task DeleteJob(Guid id)
        {
            if (_context.JobDescriptions == null)
            {
                throw new Exception("context not found");
            }
            var job = _context.JobDescriptions.Find(id);
            if (job == null)
            {
                return;
            }

            _context.JobDescriptions.Remove(job);
            await _context.SaveChangesAsync();

            return;
        }
        private bool JobExists(Guid id)
        {
            return (_context.JobDescriptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}