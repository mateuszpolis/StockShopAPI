using System;
using Dapper;
using StockShopAPI.Helpers;
using StockShopAPI.Models;

namespace StockShopAPI.Repositories
{
    public class CartRepository
    {
        private DataContext _context;
        public CartRepository(DataContext context)
        {
            _context = context;
        }

        public async Task CreateCart(Cart cart)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
                INSERT INTO Carts (UserId, TotalAmount, TotalQuantity)
                VALUES (@UserId, @TotalAmount, @TotalQuantity)
            ";
            await connection.ExecuteAsync(sql, cart);
        }

        public async Task<IEnumerable<Cart>> GetAllCarts()
        {
            using var connection = _context.CreateConnection();
            var sql = @"
                SELECT * FROM Carts
            ";
            return await connection.QueryAsync<Cart>(sql);
        }
    }
}

