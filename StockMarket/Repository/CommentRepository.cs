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

        public async Task<Comments> CreateCommentAsync(Comments comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comments?> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.ID == id);
            if (comment == null) return null;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync(true);
            return comment;
        }

        public async Task<Comments?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<List<Comments>> GetCommentsAsync()
        {
            return await _context.Comments.Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Comments?> UpdateCommentAsync(int id, Comments commentModel)
        {
            var comment = await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(x => x.ID == id);
            if (comment == null) return null;

            comment.Title = commentModel.Title;
            comment.Content = commentModel.Content;
            await _context.SaveChangesAsync();
            return comment;

        }
    }
}
