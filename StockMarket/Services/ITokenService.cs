using StockMarket.Models;

namespace StockMarket.Services
{
    public interface ITokenService
    {
        public string CreateToken(AppUser appUser);
    }
}
