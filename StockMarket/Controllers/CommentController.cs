using Microsoft.AspNetCore.Mvc;
using StockMarket.Interfaces;
using StockMarket.Mappers;
using System.Threading.Tasks;

namespace StockMarket.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comment = await _commentRepository.GetCommentsAsync();
            var commentDto = comment.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetCommentsById(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment.ToCommentDto());
        }
    }
}
