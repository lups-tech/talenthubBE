using talenthubBE.Models.Jobs;

namespace talenthubBE.Data
{
    public interface IJobsRepository
    {
        Task<IEnumerable<JobDTO>?> GetAllJobs();
        Task<JobDTO?> GetJob(Guid id);
        Task<JobDTO?> PutJob(Guid id, Job job);
        Task<JobDTO?> PostJob(String userId, String orgId, CreateJobRequest request);
        Task DeleteJob(Guid id);
    }
}