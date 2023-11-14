using talenthubBE.Models;

namespace talenthubBE.Data.Repositories.Process
{
    public interface IMatchingProcessRepository
    {
        Task<IEnumerable<MatchingProcessDTO>?> GetAllProcesses(string userId);
        Task<MatchingProcessDTO?> GetProcess(Guid id);
        Task<MatchingProcessDTO?> PostProcess(string userId, CreateProcessRequest request);
        Task<MatchingProcessDTO?> PatchProcess(EditProcessRequest request);
        Task<MatchingProcessDTO?> PatchProposed(Guid processId, ProposedDataDTO request);
        Task<MatchingProcessDTO?> PatchInterview(Guid processId, InterviewDataDTO request);
        Task<MatchingProcessDTO?> PatchContract(Guid processId, ContractDataDTO request);
        Task DeleteProcess(Guid id);
        Task DeleteProposal(Guid id);
        Task DeleteInterview(Guid id);
        Task DeleteContract(Guid id);
    }
}