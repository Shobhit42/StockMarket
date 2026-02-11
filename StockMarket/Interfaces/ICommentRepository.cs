using StockMarket.Models;

namespace StockMarket.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comments>> GetCommentsAsync();
        Task<Comments?> GetByIdAsync(int id);
        Task<Comments> CreateCommentAsync(Comments comment);
        Task<Comments?> UpdateCommentAsync(int id, Comments commentModel);
        Task<Comments?> DeleteCommentAsync(int id);
    }
}
