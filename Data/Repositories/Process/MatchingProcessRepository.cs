
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
            
            if(request.Proposed != null)
            {
                _context.Proposals.Add(ProposalConverter(request.Proposed, process));
                await _context.SaveChangesAsync();
                process.Proposed = request.Proposed.ToProposed(process);
            }
            if(request.Interviews.Any())
            {
                _context.Interviews.AddRange(InterviewConverter(request.Interviews, process));
            }
            if(request.Contracts.Any())
            {
                _context.Contracts.AddRange(ContractConverter(request.Contracts, process));
            }
            if(request.Placed != null)
            {
                process.Placed = request.Placed;
            }
            if(request.ResultDate != null)
            {
                process.ResultDate = request.ResultDate;
            }
            await _context.SaveChangesAsync();
            return process.ToMatchingProcessDTO();
        }
        public async Task<MatchingProcessDTO?> EditProcess(EditProcessRequest request)
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
            
            if(request.Proposed == null)
            {
                process.Proposed = null;
            }
            if(request.Interviews.Any())
            {
                var interviewsToRemove = process.Interviews
                    .IntersectBy(request.Interviews.Select(i => i.Id), i => i.Id);
                _context.Interviews.RemoveRange(interviewsToRemove);
            }
            if(request.Contracts.Any())
            {
                var ContractsToRemove = process.Contracts
                    .IntersectBy(request.Contracts.Select(i => i.Id), i => i.Id);
                    _context.Contracts.RemoveRange(ContractsToRemove);
            }
            if(request.Placed == null)
            {
                process.Placed = null;
            }
            if(request.ResultDate == null)
            {
                process.ResultDate = null;
            }
            _context.Entry(process).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return process.ToMatchingProcessDTO();
        }

        public async Task DeleteProcess(Guid id)
        {
            if (_context.MatchingProcesses == null)
            {
                throw new Exception("context not found");
            }
            var process = _context.MatchingProcesses.Find(id) ?? 
                throw new Exception("Developer not found");
            _context.MatchingProcesses.Remove(process);
            await _context.SaveChangesAsync();

            return;
        }
        private List<ContractData> ContractConverter(IEnumerable<ContractDataDTO> contracts, MatchingProcess process)
        {
            List<ContractData> output = new();
            foreach(ContractDataDTO dto in contracts)
            {
                output.Add(dto.ToContract(process));
            }
            return output;
        }
        private List<InterviewData> InterviewConverter(IEnumerable<InterviewDataDTO> interviews, MatchingProcess process)
        {
            List<InterviewData> output = new();
            foreach(InterviewDataDTO dto in interviews)
            {
                output.Add(dto.ToInterview(process));
            }
            return output;
        }
        private ProposedData ProposalConverter(ProposedDataDTO proposal, MatchingProcess process)
        {
            return new ProposedData
            {
                Id = proposal.Id,
                Date = proposal.Date,
                Succeeded = proposal.Succeeded,
                MatchingProcessId = process.Id,
                MatchingProcess = process,
            };
        }
    }
}