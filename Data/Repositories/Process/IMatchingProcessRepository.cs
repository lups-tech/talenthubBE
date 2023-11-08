using talenthubBE.Models;

namespace talenthubBE.Data.Repositories.Process
{
    public interface IMatchingProcessRepository
    {
        Task<IEnumerable<MatchingProcessDTO>?> GetAllProcesses(string userId);
        Task<MatchingProcessDTO?> GetProcess(Guid id);
        Task<MatchingProcessDTO?> PostProcess(string userId, CreateProcessRequest request);
        Task<MatchingProcessDTO?> PatchProcess(EditProcessRequest request);
        Task<MatchingProcessDTO?> EditProcess(EditProcessRequest request);
        Task DeleteProcess(Guid id);
    }
}