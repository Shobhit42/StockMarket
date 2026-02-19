using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarket.Data;
using StockMarket.Dtos;
using StockMarket.Helper;
using StockMarket.Interfaces;
using StockMarket.Mappers;

namespace StockMarket.Controllers
{
    [Route("stockmarket/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IStockRepository _stockRepository;
        public StockController(ApplicationDbContext applicationDbContext, IStockRepository stockRepository) 
        { 
            _context = applicationDbContext;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStocks([FromQuery] QueryObject query)
        {
            var stocks = await _stockRepository.GetAllAsync(query);
            var stockDto = stocks.Select(s => s.ToStockDto()).ToList();
            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepository.GetByIdAsync(id);
            if(stock == null) return NotFound();
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock(CreateStockDto createStockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = createStockDto.ToStockFromCreateDto();
            var createdStock = await _stockRepository.CreateStockAsync(stock);
            return CreatedAtAction(nameof(GetStock), new {id = createdStock.Id}, createdStock.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto update)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepository.UpdateStockAsync(id, update);
            if (stock == null) return NotFound();
            return Ok(stock.ToStockDto());
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepository.DeleteStockAsync(id);
            if(stock == null) return NotFound();
            return NoContent();
        }
    }
}
