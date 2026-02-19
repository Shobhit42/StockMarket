using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Interfaces;
using StockMarket.Models;

namespace StockMarket.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IportfolioRepository _portfolioRepository;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IportfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.Identity.Name;
            if (username == null)
                return Unauthorized();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return NotFound();

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(user);
            return  Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostUserPortfolio(string symbol)
        {
            var username = User.Identity.Name;
            if (username == null)
                return Unauthorized();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return NotFound();

            var stock = _stockRepository.GetBySymbol(symbol);
            if (stock == null) return BadRequest("Stock Not Found");

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(user);

            if (userPortfolio.Any(x => x.Symbol.ToLower() == symbol.ToLower())) return BadRequest("cannot add same stokc to portfolio");

            var portfolioModel = new Portfolio
            {
                AppUserId = user.Id,
                StockId = stock.Id,
            };

            await _portfolioRepository.CreateUserPortfolio(portfolioModel);
            if(portfolioModel == null)
            {
                return StatusCode(500, "Could Not create");
            }
            else{
                return Created();
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUserPortfolio(string symbol)
        {
            var username = User.Identity.Name;
            if (username == null)
                return Unauthorized();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return NotFound();

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(user);
            if (userPortfolio.Any(x => x.Symbol.ToLower() != symbol.ToLower())) return BadRequest("cannot find stokc with this symbol");

            var delPortfolio = await _portfolioRepository.DeletePortfolio(user, symbol);
            return Ok(delPortfolio);

        }

    }
}
