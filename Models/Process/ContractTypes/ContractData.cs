
namespace talenthubBE.Models
{
    public class ContractData
    {
        public Guid Id {get; set;}
        public DateTime? Date {get; set;}
        public int ContractStage {get; set;}
        public Guid MatchingProcessId {get; set;}
        public MatchingProcess MatchingProcess {get; set;} = null!;
    }
}