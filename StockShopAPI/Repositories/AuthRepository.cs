using System;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Identity;
using StockShopAPI.Helpers;
using StockShopAPI.Models;
using StockShopAPI.Models.Dto;

namespace StockShopAPI.Repositories
{
    public class AuthRepository
    {
        private DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetById(int id)
        {
            using System.Data.IDbConnection connection = _context.CreateConnection();
            var sql = @"
                SEELCT * FROM Users
                WHERE Id = @id
        
            ";
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { id });
        }

        public async Task<User> GetByEmail(string email)
        {
            using System.Data.IDbConnection connection = _context.CreateConnection();
            var sql = @"
                SELECT * FROM Users
                WHERE Email = @email
            ";
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { email });
        }

        public async Task CreateUserAndCart(User user)
        {
            using var connection = _context.CreateConnection();

            var sqlCreateUser = @"
                INSERT INTO Users (FirstName, LastName, Email, PasswordHash)
                VALUES (@FirstName, @LastName, @Email, @PasswordHash)
                RETURNING Id;
            ";

            var sqlCreateCart = @"
                INSERT INTO Carts (UserId, TotalAmount, TotalQuantity)
                VALUES (@UserId, 0, 0)
                RETURNING Id;
            ";

            try
            {
                connection.Open(); // Open the connection asynchronously

                using var transaction = connection.BeginTransaction();

                try
                {
                    var userId = await connection.ExecuteScalarAsync<int>(sqlCreateUser, user);

                    var cartId = await connection.ExecuteScalarAsync<int>(sqlCreateCart, new { UserId = userId });

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            finally
            {
                connection.Close(); // Close the connection in the finally block
            }
        }


        public async Task Update(User user)
        {
            using System.Data.IDbConnection connection = _context.CreateConnection();
            var sql = @"
                UPDATE Users
                SET FirstName = @Firstname,
                    LastName = @LastName,
                    Email = @Email,
                    PasswordHash = @PasswordHash
                WHERE Id = @Id
            ";
            await connection.ExecuteAsync(sql, user);
        }

        public async Task Delete(int id)
        {
            using System.Data.IDbConnection connection = _context.CreateConnection();
            var sql = @"
                DELETE FROM Users
                WHERE Id = @id
            ";
            await connection.ExecuteAsync(sql, id);
        }
    }
}

