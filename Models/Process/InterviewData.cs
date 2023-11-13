
namespace talenthubBE.Models
{
    public class InterviewData
    {
        public Guid Id {get; set;}
        public DateTime Date {get; set;}
        public int InterviewType {get; set;}
        public bool? Passed {get; set;}
        public Guid MatchingProcessId {get; set;}
        public MatchingProcess MatchingProcess {get; set;} = null!;
    }
}