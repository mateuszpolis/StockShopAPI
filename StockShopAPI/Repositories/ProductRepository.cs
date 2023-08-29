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

        public async Task<IEnumerable<Product>> GetProducts(string searchQuery, int category, string sorting, int limit)
        {
            using var connection = _context.CreateConnection();
            string order = "ORDER BY Orders DESC";
            if (sorting == "price_low_to_high")
            {
                order = "ORDER BY Price ASC";
            }
            else if (sorting == "price_high_to_low")
            {
                order = "ORDER BY Price DESC";
            }
            else if (sorting == "rating_high_to_low")
            {
                order = "ORDER BY Rating DESC";
            }
            IEnumerable<Category> subcategories = await GetSubcategories(category);
            var categoryIds = subcategories.Select(c => c.Id).Append(category).ToArray();
            var sql = $@"
                SELECT * FROM Products
                WHERE Name ILIKE '%' || @searchQuery || '%'
                AND CategoryId = ANY(@categoryIds)
                {order}
                LIMIT @limit
            ";

            return await connection.QueryAsync<Product>(sql, new { searchQuery, categoryIds, limit });
        }

        public async Task<IEnumerable<Category>> GetSubcategories(int category)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
                WITH RECURSIVE Subcategories AS (
                    SELECT Id, Name, Description, HasChildren, ParentCategory, Transactions, Visits
                    FROM Categories
                    WHERE Id = @category

                    UNION ALL

                    SELECT c.Id, c.Name, c.Description, c.HasChildren, c.ParentCategory, c.Transactions, c.Visits
                    FROM Categories c
                    JOIN Subcategories s ON c.ParentCategory = s.Id
                )
                SELECT Id, Name, Description, HasChildren, ParentCategory, Transactions, Visits
                FROM Subcategories
                WHERE Id <> @category;
            ";
            return await connection.QueryAsync<Category>(sql, new { category });
        }
    }
}

