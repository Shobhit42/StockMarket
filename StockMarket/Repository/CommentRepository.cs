using Microsoft.EntityFrameworkCore;
using StockMarket.Data;
using StockMarket.Interfaces;
using StockMarket.Models;

namespace StockMarket.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<Comments?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<List<Comments>> GetCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }
    }
}
