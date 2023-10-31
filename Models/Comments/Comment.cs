using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace talenthubBE.Models.Comments
{
    public class Comment
    {
        [Column("id")]
        public required Guid Id { get; init; }
        [Column("comment_text")]
        public String? CommentText { get; set; }
        [Column("created_at")]
        public required DateTime CreatedAt { get; init; }
        [Column("user_email")]
        public String? UserEmail { get; set; }
        [Column("user_id")]
        public String? UserId { get; set; }

        public User User { get; set; } = null!;
        [Column("developer_id")]
        public Guid DeveloperId {get; set;}
        public Developer Developer {get; set; } = null!;
    }
}