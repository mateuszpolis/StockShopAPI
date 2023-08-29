using System;
using Dapper;
using StockShopAPI.Helpers;
using StockShopAPI.Models;

namespace StockShopAPI.Repositories
{
    public class ReviewRepository
    {

        private DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(Review review)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
                INSERT INTO Reviews (UserId, ProductId, Rating, ReviewText, Likes, OwnsProduct, CreatedTime)
                VALUES (@UserId, @ProductId, @Rating, @ReviewText, @Likes, @OwnsProduct, @CreatedTime)
            ";
            await connection.ExecuteAsync(sql, review);
        }

        public async Task<IEnumerable<Review>> GetReviews(int productId)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
                SELECT * FROM Reviews
                WHERE ProductId = @productId
            ";
            return await connection.QueryAsync<Review>(sql, new { productId });
        }
    }
}

