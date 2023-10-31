using System.ComponentModel.DataAnnotations;

namespace talenthubBE.Models.Comments
{
    public class CreateCommentRequest
    {
        [Required]
        public String? CommentText { get; set; }
        [Required]
        public String? UserEmail { get; set; }
        [Required]
        public Guid DeveloperId {get; set;}
    }
}