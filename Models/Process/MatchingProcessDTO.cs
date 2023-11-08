namespace talenthubBE.Models
{
    public class MatchingProcessDTO
    {
        public Guid Id {get; set;}
        public ProposedDataDTO? Proposed {get; set;}
        public List<InterviewDataDTO>? Interviews {get; set;}
        public List<ContractDataDTO>? Contracts {get; set;}
        public bool? Placed { get; set; }
        public DateTime? ResultDate {get; set;}
        public required Guid DeveloperId { get; set; }
        public required Guid JobId { get; set; }
        public required string UserId { get; set; }
    }
}