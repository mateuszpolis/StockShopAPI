using System;
using Dapper;
using StockShopAPI.Helpers;
using StockShopAPI.Models;

namespace StockShopAPI.Repositories
{
    public class ProductRepository
    {
        private DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Product> GetById(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
                SELECT * FROM Products
                WHERE Id = @id
            ";
            return await connection.QuerySingleOrDefaultAsync<Product>(sql, new { id });
        }

        public async Task Create(Product product)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
                INSERT INTO Products (Name, Brand, Price, Description, Discount, StockQuantity, Availability, CreatedTime, UpdatedTime, Weight, Dimensions, Rating, Orders, CategoryId)
                VALUES (@Name, @Brand, @Price, @Description, @Discount, @StockQuantity, @Availability, @CreatedTime, @UpdatedTime, @Weight, @Dimensions, @Rating, @Orders, @CategoryId)
                
            ";
            await connection.ExecuteAsync(sql, product);
        }

        public async Task<IEnumerable<Product>> GetProducts(string searchQuery, int limit)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
                SELECT * FROM Products
                WHERE Name ILIKE '%' || @searchQuery || '%'
                ORDER BY Orders DESC
                LIMIT @limit
            ";
            return await connection.QueryAsync<Product>(sql, new { searchQuery, limit });
        }
    }
}

