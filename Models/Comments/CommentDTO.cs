namespace talenthubBE.Models.Comments
{
    public class CommentDTO
    {
        public required Guid Id { get; set; }
        public String? CommentText { get; set; }
        public required DateTime CreatedAt { get; set; }
        public String? UserEmail { get; set; }
        public String? UserId { get; set; }
        public Guid DeveloperId {get; set;}
    }
}