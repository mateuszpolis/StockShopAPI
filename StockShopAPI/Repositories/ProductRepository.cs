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
                INSERT INTO Products (Name, Brand, Price, Description, Discount, StockQuantity, Availability, CreatedTime, UpdatedTime, Weight, Dimensions, Rating)
                VALUES (@Name, @Brand, @Price, @Description, @Discount, @StockQuantity, @Availability, @CreatedTime, @UpdatedTime, @Weight, @Dimensions, @Rating)
                
            ";
            await connection.ExecuteAsync(sql, product);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            using var connection = _context.CreateConnection();
            var sql = @"
                SELECT * FROM Products
            ";
            return await connection.QueryAsync<Product>(sql);
        }
    }
}

