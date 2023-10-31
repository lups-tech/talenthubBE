using Microsoft.EntityFrameworkCore;
using talenthubBE.Mapping;
using talenthubBE.Models;
using talenthubBE.Models.Comments;

namespace talenthubBE.Data.Repositories.Comments
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly MvcDataContext _context;
        public CommentsRepository(MvcDataContext context) => _context = context;

        public async Task<IEnumerable<CommentDTO>?> GetAllUserComments(string userId)
        {
            if (_context.Comments == null)
            {
                return null;
            }
            var res = await _context.Comments
                .Where(u => u.UserId == userId)
                .ToListAsync();

            List<CommentDTO> comments = new();
            foreach (Comment comment in res)
            {
                comments.Add(comment.ToCommentDTO());
            }
            return comments;
        }

        public async Task<CommentDTO?> GetComment(Guid id)
        {
            if (_context.Comments == null)
            {
                return null;
            }
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return null;
            }

            return comment.ToCommentDTO();
        }

        public async Task<CommentDTO?> PostComment(string userId, Guid developerId, CreateCommentRequest request)
        {
            if (_context.Comments == null)
            {
                return null;
            }
            User user = await _context.Users.SingleAsync(u => u.Id == userId);
            Developer developer = await _context.Developers.SingleAsync(d=> d.Id == developerId);
            
            Comment comment = request.ToComment(user, developer);
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return comment.ToCommentDTO();
        }

        public async Task<CommentDTO?> PutComment(Guid id, Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return comment.ToCommentDTO();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task DeleteComment(Guid id)
        {
            if (_context.Comments == null)
            {
                throw new Exception("context not found");
            }
            var comment = _context.Comments.Find(id) ?? throw new Exception("Comment not found");
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return;
        }
            private bool CommentExists(Guid id)
        {
            return (_context.Comments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}