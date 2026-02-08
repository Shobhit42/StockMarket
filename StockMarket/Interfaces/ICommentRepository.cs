using StockMarket.Models;

namespace StockMarket.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comments>> GetCommentsAsync();
        Task<Comments?> GetByIdAsync(int id);
    }
}
