using System.Drawing;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using talenthubBE.Data;
using talenthubBE.Data.Repositories.Comments;
using talenthubBE.Helpers;
using talenthubBE.Models.Comments;

namespace talenthubBE.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsRepository _repository;

        public CommentsController(ICommentsRepository CommentsRepository)
        {
            _repository = CommentsRepository;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetUserComments()
        {
            string userId = ControllerHelper.UserIdFinder(User);
            IEnumerable<CommentDTO>? response = await _repository.GetAllUserComments(userId);
                if(response == null)
                {
                    return NotFound();
                }
            return Ok(response);
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetComment(Guid id)
        {
            CommentDTO? response = await _repository.GetComment(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<CommentDTO>> PutComment(Guid id, Comment Comment)
        {
            if (id != Comment.Id)
            {
                return BadRequest();
            }
            CommentDTO? response = await _repository.PutComment(id, Comment);
            if(response == null)
            {
                return NotFound(new {message = "No such Comment found"});
            }
            try
            {
                return Ok(response);
            }
            catch(Exception)
            {
                return Conflict(new {message = "There has been an issue handling your request"});
            }
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CommentDTO>> PostComment(CreateCommentRequest request, Guid developerId)
        {
            string userId = ControllerHelper.UserIdFinder(User);

            CommentDTO? response = await _repository.PostComment(userId, developerId, request);
            if(response == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetComment", new { id = response.Id }, response);
        }
        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            try
            {
                await _repository.DeleteComment(id);
            }
            catch (Exception e)
            {
                return NotFound(new {message = e.Message});
            }
            return NoContent();
        }
    }
}
