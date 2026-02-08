using StockMarket.Dtos;
using StockMarket.Models;
using System.Runtime.CompilerServices;

namespace StockMarket.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto()
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchanse = stockModel.Purchanse,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList(),
            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockDto createStockModel)
        {
            return new Stock()
            {
                Symbol = createStockModel.Symbol,
                CompanyName = createStockModel.CompanyName,
                Purchanse = createStockModel.Purchanse,
                LastDiv = createStockModel.LastDiv,
                Industry = createStockModel.Industry,
                MarketCap = createStockModel.MarketCap
            };
        }
    }
}
