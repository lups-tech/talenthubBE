using talenthubBE.Models;

namespace talenthubBE.Mapping
{
    public static class ProcessAPIMapper
    {
        public static MatchingProcess ToProcess(this CreateProcessRequest request, Developer developer, Job job, User user)
        {
            return new MatchingProcess
            {
                Id = new Guid(),
                DeveloperId = request.DeveloperId,
                Developer = developer,
                JobId = request.JobId,
                Job = job,
                UserId = user.Id,
                User = user,
                CreatedAt = DateTime.Now.ToUniversalTime(),
            };
        }
        public static MatchingProcessDTO ToMatchingProcessDTO(this MatchingProcess process)
        {

            return new MatchingProcessDTO
            {
                Id = process.Id,
                Proposed = ProposedChecker(process.Proposed),
                Interviews = process.Interviews?.InterviewTypeMatcher().OrderBy(i => i.Date).ToList(),
                Contracts = process.Contracts?.ContractTypeMatcher().OrderBy(i => i.Date).ToList(),
                DeveloperId = process.DeveloperId,
                JobId = process.JobId,
                UserId = process.UserId,
                ResultDate = process.ResultDate,
                Placed = process.Placed,
            };
        }

        public static ProposedData ToProposed(this ProposedDataDTO proposed, MatchingProcess process)
        {
            return new ProposedData
            {
                Id = proposed.Id,
                Date = proposed.Date,
                Succeeded = proposed.Succeeded,
                MatchingProcessId = process.Id,
                MatchingProcess = process
            };
        }

        public static InterviewData ToInterview(this InterviewDataDTO interview, MatchingProcess process)
        {
            var interviewType = Enum.Parse(typeof(InterviewTypes), interview.InterviewType);
            return new InterviewData
            {
                Id = interview.Id,
                Date = interview.Date,
                InterviewType = (int)interviewType,
                Passed = interview.Passed,
                MatchingProcessId = process.Id,
                MatchingProcess = process
            };
        }

        public static ContractData ToContract(this ContractDataDTO proposed, MatchingProcess process)
        {
            var stage = Enum.Parse(typeof(ContractStages), proposed.ContractStage);
            return new ContractData
            {
                Id = proposed.Id,
                Date = proposed.Date,
                ContractStage = (int)stage,
                MatchingProcessId = process.Id,
                MatchingProcess = process
            };
        }

        private static ProposedDataDTO? ProposedChecker(ProposedData? proposedData)
        {
            if (proposedData == null)
            {
                return null;
            }
            return proposedData.ToProposedDTO();
        }
        private static ProposedDataDTO ToProposedDTO(this ProposedData proposed)
        {
            return new ProposedDataDTO
            {
                Id = proposed.Id,
                Date = proposed.Date,
                Succeeded = proposed.Succeeded,
            };
        }
        private static List<InterviewDataDTO> InterviewTypeMatcher(this IEnumerable<InterviewData> interviews)
        {
            List<InterviewDataDTO> output = new();
            foreach(InterviewData interview in interviews)
            {
                output.Add(interview.ToInterviewDTO());
            }
            return output;
        }
        private static InterviewDataDTO ToInterviewDTO(this InterviewData interview)
        {
            return new InterviewDataDTO
            {
                Id = interview.Id,
                Date = interview.Date,
                InterviewType = Enum.GetName(typeof(InterviewTypes), interview.InterviewType)!,
                Passed = interview.Passed,
            };
        }
        private static List<ContractDataDTO> ContractTypeMatcher(this IEnumerable<ContractData> contracts)
        {
            List<ContractDataDTO> output = new();
            foreach(ContractData contract in contracts)
            {
                output.Add(contract.ToContractDataDTO());
            }
            return output;
        }
        private static ContractDataDTO ToContractDataDTO(this ContractData contract)
        {
            return new ContractDataDTO
            {
                Id = contract.Id,
                Date = contract.Date,
                ContractStage = Enum.GetName(typeof(ContractStages), contract.ContractStage)!,
            };
        }
    }
}