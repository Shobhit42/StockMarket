using StockMarket.Dtos;
using StockMarket.Models;

namespace StockMarket.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comments comments)
        {
            return new CommentDto()
            {
                ID = comments.ID,
                Title = comments.Title,
                Content = comments.Content,
                CreatedOn = comments.CreatedOn,
                StockID = comments.StockID
            };
        }

        public static Comments ToCommentFromCreate(this CreateCommentDto commentDto, int stockId)
        {
            return new Comments
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockID = stockId
            };
        }

        public static Comments ToCommentFromUpdate(this UpdateCommentRequestDtos commentDto)
        {
            return new Comments
            {
                Title = commentDto.Title,
                Content = commentDto.Content
            };
        }

    }
}
