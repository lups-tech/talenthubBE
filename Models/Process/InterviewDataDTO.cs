namespace talenthubBE.Models
{
    public class InterviewDataDTO
    {
        public Guid Id {get; set;}
        public DateTime Date {get; set;}
        public required string InterviewType {get; set;}
        public bool Passed {get; set;}
    }
}