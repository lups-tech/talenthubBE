using talenthubBE.Models;
using talenthubBE.Models.Comments;

namespace talenthubBE.Mapping
{
    public static class CommentAPIMapper
    {
        public static Comment ToComment(this CreateCommentRequest request, User user, Developer developer)
        {
            return new Comment
            {
                Id = new Guid(),
                CommentText = request.CommentText,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                UserEmail = request.UserEmail,
                DeveloperId = developer.Id,
                Developer = developer,
                UserId = user.Id,
                User = user,
            };
        }
        public static CommentDTO ToCommentDTO(this Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                CommentText = comment.CommentText,
                CreatedAt = comment.CreatedAt,
                UserEmail = comment.UserEmail,
                DeveloperId = comment.DeveloperId,
                UserId = comment.UserId,
            };
        }
    }
}