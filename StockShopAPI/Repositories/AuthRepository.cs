using System;
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

        public async Task Create(User user)
        {
            using System.Data.IDbConnection connection = _context.CreateConnection();
            var sql = @"
                INSERT INTO Users (FirstName, LastName, Email, PasswordHash)
                VALUES (@FirstName, @LastName, @Email, @PasswordHash)
            ";
            await connection.ExecuteAsync(sql, new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = user.PasswordHash
            });
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

