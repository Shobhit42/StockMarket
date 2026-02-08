using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarket.Data;
using StockMarket.Dtos;
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
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _context.Stocks.Include(c => c.Comments).Select(stocks => stocks.ToStockDto()).ToListAsync();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock([FromRoute] int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if(stock == null) return NotFound();
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock(CreateStockDto createStockDto)
        {
            var stock = createStockDto.ToStockFromCreateDto();
            var createdStock = await _stockRepository.CreateStockAsync(stock);
            return CreatedAtAction(nameof(GetStock), new {id = createdStock.Id}, createdStock.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto update)
        {
            var stock = await _stockRepository.UpdateStockAsync(id, update);
            if (stock == null) return NotFound();
            return Ok(stock.ToStockDto());
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepository.DeleteStockAsync(id);
            if(stock == null) return NotFound();
            return NoContent();
        }
    }
}
