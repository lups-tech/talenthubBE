using talenthubBE.Models.Jobs;

namespace talenthubBE.Data
{
    public interface IJobsRepository
    {
        Task<IEnumerable<JobDTO>?> GetAllJobs(String orgId);
        Task<JobDTO?> GetJob(Guid id, String orgId);
        Task<JobDTO?> PutJob(Guid id, Job job);
        Task<JobDTO?> PostJob(String userId, String orgId, CreateJobRequest request);
        Task DeleteJob(Guid id);
    }
}