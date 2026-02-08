using Microsoft.EntityFrameworkCore;
using StockMarket.Data;
using StockMarket.Dtos;
using StockMarket.Interfaces;
using StockMarket.Models;

namespace StockMarket.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext applicationDbContext) 
        {
            _context = applicationDbContext;
        }
        public Task<List<Stock>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto updateStock)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null) return null;

            stock.Symbol = updateStock.Symbol;
            stock.CompanyName = updateStock.CompanyName;
            stock.Purchanse = updateStock.Purchanse;
            stock.LastDiv = updateStock.LastDiv;
            stock.Industry = updateStock.Industry;
            stock.MarketCap = updateStock.MarketCap;
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null) { return null; }
            _context.Stocks.Remove(stock); 
            await _context.SaveChangesAsync();
            return stock;

        }
    }
}
