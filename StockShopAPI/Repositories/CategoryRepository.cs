using System;
using StockShopAPI.Helpers;
using StockShopAPI.Models;
using Dapper;
using System.Data;

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

        public async Task<IEnumerable<Category>> GetCategoryHierarchy(int categoryId)
        {
            using var connection = _context.CreateConnection();

            var sql = @"
                WITH RECURSIVE CategoryCTE AS (
                    SELECT Id, Name, Description, ParentCategory
                    FROM Categories
                    WHERE Id = @categoryId
                    UNION
                    SELECT c.Id, c.Name, c.Description, c.ParentCategory
                    FROM Categories c
                    JOIN CategoryCTE cte ON c.Id = cte.ParentCategory
                )
                SELECT Id, Name, Description, ParentCategory
                FROM CategoryCTE;
            ";

            return await connection.QueryAsync<Category>(sql, new { categoryId });
        }

        public async Task Create(Category category)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
				INSERT INTO Categories (Name, Description, ParentCategory, HasChildren, Transactions, Visits)
				VALUES (@Name, @Description, @ParentCategory, @HasChildren, @Transactions, @Visits);

                UPDATE Categories
                SET HasChildren = true
                WHERE Id = @ParentCategory AND @ParentCategory IS NOT NULL;
			";
            await connection.ExecuteAsync(sql, category);
        }
    }
}

