using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Dtos;
using StockMarket.Interfaces;
using StockMarket.Mappers;
using StockMarket.Models;
using System.Threading.Tasks;

namespace StockMarket.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.GetCommentsAsync();
            var commentDto = comment.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCommentsById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        [Route("{stokId:int}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stokId, [FromBody] CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _stockRepository.StockExists(stokId))
                return BadRequest("Stock does not exists");

            var user = User.Identity.Name;
            var appUser = await _userManager.FindByNameAsync(user);

            var commentModel = createCommentDto.ToCommentFromCreate(stokId);
            commentModel.AppUerId = appUser.Id;
            await _commentRepository.CreateCommentAsync(commentModel);
            return CreatedAtAction(nameof(GetCommentsById), new { id = commentModel.ID }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDtos updateCommentRequestDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentModel = updateCommentRequestDtos.ToCommentFromUpdate();
            var comment = await _commentRepository.UpdateCommentAsync(id, commentModel);
            if (comment == null) return NotFound();
            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commnetModel = await _commentRepository.DeleteCommentAsync(id);
            if(commnetModel == null) return NotFound();
            return Ok(commnetModel.ToCommentDto());
        }

    }
}
