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
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
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
        }

    }
}
