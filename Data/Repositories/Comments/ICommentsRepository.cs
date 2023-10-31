using talenthubBE.Models.Comments;

namespace talenthubBE.Data.Repositories.Comments
{
    public interface ICommentsRepository
    {
        Task<IEnumerable<CommentDTO>?> GetAllUserComments(string userId);
        Task<CommentDTO?> GetComment(Guid id);
        Task<CommentDTO?> PutComment(Guid id, Comment comment);
        Task<CommentDTO?> PostComment(string userId, Guid developerId, CreateCommentRequest request);
        Task DeleteComment(Guid id);
    }
}