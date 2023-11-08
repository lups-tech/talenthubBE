using System.ComponentModel.DataAnnotations.Schema;

namespace talenthubBE.Models
{
    public class MatchingProcess
    {
        [Column("id")]
        public Guid Id {get; set;}
        [Column("proposed")]
        public ProposedData? Proposed {get; set;}
        [Column("interviews")]
        public ICollection<InterviewData>? Interviews {get; set;} = new List<InterviewData>();
        [Column("contracts")]
        public ICollection<ContractData>? Contracts {get; set;} = new List<ContractData>();
        [Column("placed")]
        public bool? Placed { get; set; }
        [Column("result_date")]
        public DateTime? ResultDate {get; set;}
        [Column("created_at")]
        public DateTime CreatedAt {get; set;}
        public required Guid DeveloperId { get; set; }
        public required Developer Developer { get; set; } = null!;
        public required Guid JobId { get; set; }
        public required Job Job { get; set; } = null!;
        public required string UserId { get; set; }
        public required User User { get; set; } = null!;
    }
}