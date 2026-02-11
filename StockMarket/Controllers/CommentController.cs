using Microsoft.AspNetCore.Mvc;
using StockMarket.Dtos;
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
        private readonly IStockRepository _stockRepository;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
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

        [HttpPost]
        [Route("{stokId}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stokId, [FromBody] CreateCommentDto createCommentDto)
        {
            if (!await _stockRepository.StockExists(stokId))
                return BadRequest("Stock does not exxists");

            var commentModel = createCommentDto.ToCommentFromCreate(stokId);
            await _commentRepository.CreateCommentAsync(commentModel);
            return CreatedAtAction(nameof(GetCommentsById), new { id = commentModel.ID }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDtos updateCommentRequestDtos)
        {
            var commentModel = updateCommentRequestDtos.ToCommentFromUpdate();
            var comment = await _commentRepository.UpdateCommentAsync(id, commentModel);
            if (comment == null) return NotFound();
            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var commnetModel = await _commentRepository.DeleteCommentAsync(id);
            if(commnetModel == null) return NotFound();
            return Ok(commnetModel.ToCommentDto());
        }

    }
}
