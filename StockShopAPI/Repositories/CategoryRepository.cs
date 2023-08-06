using System;
using StockShopAPI.Helpers;
using StockShopAPI.Models;
using Dapper;

namespace StockShopAPI.Repositories
{
    public class CategoryRepository
    {
        private DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories(int ParentId)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
				SELECT * FROM Categories
				WHERE ParentCategory = @ParentId
			";
            if (ParentId == -1)
            {
                sql = @"
					SELECT * FROM Categories
					WHERE ParentCategory IS NULL
				";
            }
            return await connection.QueryAsync<Category>(sql, new { ParentId });
        }

        public async Task Create(Category category)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
				INSERT INTO Categories (Name, Description, ParentCategory, HasChildren)
				VALUES (@Name, @Description, @ParentCategory, @HasChildren);

                UPDATE Categories
                SET HasChildren = true
                WHERE Id = @ParentCategory AND @ParentCategory IS NOT NULL;
			";
            await connection.ExecuteAsync(sql, category);
        }
    }
}

