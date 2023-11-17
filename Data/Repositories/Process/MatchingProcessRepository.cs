
using System.Diagnostics.Contracts;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using talenthubBE.Mapping;
using talenthubBE.Models;

namespace talenthubBE.Data.Repositories.Process
{
    public class MatchingProcessRepository : IMatchingProcessRepository
    {
        private readonly MvcDataContext _context;
        public MatchingProcessRepository(MvcDataContext context) => _context = context;

        public async Task<IEnumerable<MatchingProcessDTO>?> GetAllProcesses(string userId)
        {
            if (_context.MatchingProcesses == null)
            {
                return null;
            }
            var res = await _context.MatchingProcesses
                .Include(p => p.Proposed)
                .Include(p => p.Interviews)
                .Include(p => p.Contracts)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            List<MatchingProcessDTO> processes = new();
            foreach (MatchingProcess process in res)
            {
                processes.Add(process.ToMatchingProcessDTO());
            }

            return processes;
        }

        public async Task<MatchingProcessDTO?> GetProcess(Guid id)
        {
            if (_context.MatchingProcesses == null)
            {
                return null;
            }
            var process = await _context.MatchingProcesses
                .Include(p => p.Proposed)
                .Include(p => p.Interviews)
                .Include(p => p.Contracts)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (process == null)
            {
                return null;
            }

            return process.ToMatchingProcessDTO();
        }

        public async Task<MatchingProcessDTO?> PostProcess(string userId, CreateProcessRequest request)
        {
            if (_context.MatchingProcesses == null)
            {
              return null;
            }
            Developer developer = await _context.Developers.SingleAsync<Developer>(d => d.Id == request.DeveloperId);
            Job job = await _context.JobDescriptions.SingleAsync<Job>(j => j.Id == request.JobId);
            User user = await _context.Users.SingleAsync<User>(u => u.Id == userId);

            MatchingProcess process = request.ToProcess(developer, job, user);
            _context.MatchingProcesses.Add(process);
            await _context.SaveChangesAsync();

            return process.ToMatchingProcessDTO();
        }

        public async Task<MatchingProcessDTO?> PatchProcess(EditProcessRequest request)
        {
            if (_context.MatchingProcesses == null)
            {
              return null;
            }
            MatchingProcess process = await _context.MatchingProcesses
                .Include(p => p.Proposed)
                .Include(p => p.Interviews)
                .Include(p => p.Contracts)
                .SingleAsync<MatchingProcess>(p => p.Id == request.Id);
            
            process.Placed = request.Placed;
            process.ResultDate = request.ResultDate;
         
            await _context.SaveChangesAsync();
            return process.ToMatchingProcessDTO();
        }
        public async Task<MatchingProcessDTO?> PatchProposed(Guid processId, ProposedDataDTO request)
        {
             MatchingProcess? process = await _context.MatchingProcesses
                .Include(p => p.Proposed)
                .Include(p => p.Interviews)
                .Include(p => p.Contracts)
                .SingleOrDefaultAsync(p => p.Id == processId) 
                ?? throw new Exception("Matching Process not found");
            
            if(!_context.Proposals.Any(i => i.Id == request.Id))
            {
                ProposedData newProposed = request.ToProposed(process);
                _context.Proposals.Add(newProposed);
                await _context.SaveChangesAsync();
            }
            else 
            {
                ProposedData proposalToPatch = await _context.Proposals
                    .SingleOrDefaultAsync(i => i.Id == request.Id)
                    ?? throw new Exception("Proposal not found");
                
                proposalToPatch.Date = request.Date;
                proposalToPatch.Succeeded = request.Succeeded;
                await _context.SaveChangesAsync();
            }
            return process.ToMatchingProcessDTO();
        }
        
        public async Task<MatchingProcessDTO?> PatchInterview(Guid processId, InterviewDataDTO request)
        {
            MatchingProcess? process = await _context.MatchingProcesses
                .Include(p => p.Proposed)
                .Include(p => p.Interviews)
                .Include(p => p.Contracts)
                .SingleOrDefaultAsync(p => p.Id == processId) 
                ?? throw new Exception("Matching Process not found");
            
            if(!_context.Interviews.Any(i => i.Id == request.Id))
            {
                InterviewData newInterview = request.ToInterview(process);
                _context.Interviews.Add(newInterview);
                await _context.SaveChangesAsync();
            }
            else 
            {
                InterviewData interviewToPatch = await _context.Interviews
                    .SingleOrDefaultAsync(i => i.Id == request.Id)
                    ?? throw new Exception("Interview not found");
                
                interviewToPatch.InterviewType = (int)Enum.Parse(typeof(InterviewTypes), request.InterviewType);
                interviewToPatch.Date = request.Date;
                interviewToPatch.Passed = request.Passed;
                await _context.SaveChangesAsync();
            }
            return process.ToMatchingProcessDTO();
        }

        public async Task<MatchingProcessDTO?> PatchContract(Guid processId, ContractDataDTO request)
        {
             MatchingProcess? process = await _context.MatchingProcesses
                .Include(p => p.Proposed)
                .Include(p => p.Interviews)
                .Include(p => p.Contracts)
                .SingleOrDefaultAsync(p => p.Id == processId) 
                ?? throw new Exception("Matching Process not found");
            
            if(!_context.Contracts.Any(i => i.Id == request.Id))
            {
                ContractData newContract = request.ToContract(process);
                _context.Contracts.Add(newContract);
                await _context.SaveChangesAsync();
            }
            else 
            {
                ContractData ContractToPatch = await _context.Contracts
                    .SingleOrDefaultAsync(i => i.Id == request.Id)
                    ?? throw new Exception("Contract not found");
                
                ContractToPatch.Date = request.Date;
                ContractToPatch.ContractStage = (int)Enum.Parse(typeof(ContractStages), request.ContractStage);
                await _context.SaveChangesAsync();
            }
            return process.ToMatchingProcessDTO();
        }
        public async Task DeleteProcess(Guid id)
        {
            if (_context.MatchingProcesses == null)
            {
                throw new Exception("context not found");
            }
            var process = _context.MatchingProcesses.Find(id) ?? 
                throw new Exception("Process not found");
            _context.MatchingProcesses.Remove(process);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task DeleteProposal(Guid id)
        {
            if (_context.Proposals == null)
            {
                throw new Exception("context not found");
            }
            var proposal = _context.Proposals.Find(id) ?? 
                throw new Exception("Proposal not found");
            _context.Proposals.Remove(proposal);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task DeleteInterview(Guid id)
        {
            if (_context.Interviews == null)
            {
                throw new Exception("context not found");
            }
            var interview = _context.Interviews.Find(id) ?? 
                throw new Exception("Interview not found");
            _context.Interviews.Remove(interview);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task DeleteContract(Guid id)
        {
            if (_context.Contracts == null)
            {
                throw new Exception("context not found");
            }
            var contract = _context.Contracts.Find(id) ?? 
                throw new Exception("Contract not found");
            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return;
        }
    }
}