using System;
using Dapper;
using StockShopAPI.Helpers;
using StockShopAPI.Models;
using StockShopAPI.Models.Dto;

namespace StockShopAPI.Repositories
{
    public class ReviewRepository
    {

        private DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(ReviewCreateDTO review)
        {
            using var connection = _context.CreateConnection();

            var getUserSql = @"
                SELECT Id, FirstName, LastName
                FROM Users
                WHERE Id = @UserId
            ";
            var user = await connection.QuerySingleOrDefaultAsync<UserNameSurnameDTO>(getUserSql, new { review.UserId });

            if (user != null)
            {
                var insertReviewSql = @"
                    INSERT INTO Reviews (UserId, UserFirstName, UserLastName, ProductId, Rating, ReviewText)
                    VALUES (@UserId, @UserFirstName, @UserLastName, @ProductId, @Rating, @ReviewText)
                ";
                await connection.ExecuteAsync(insertReviewSql, new
                {
                    UserId = review.UserId,
                    UserFirstName = user.FirstName,
                    UserLastName = user.LastName,
                    ProductId = review.ProductId,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }
            else
            {
                return false;
            }

            var getProductSql = @"
                SELECT Id, Rating, NumberOfReviews
                FROM Products
                WHERE Id = @ProductId
            ";
            var product = await connection.QuerySingleOrDefaultAsync<ProductReviewInfo>(getProductSql, new { review.ProductId });

            if (product != null)
            {
                var newNumOfReviews = product.NumberOfReviews + 1;
                var newRating = ((product.Rating * product.NumberOfReviews) + review.Rating) / newNumOfReviews;

                var updateProductSql = @"
                    UPDATE Products
                    SET Rating = @newRating, NumberOfReviews = @newNumOfReviews
                    WHERE Id = @ProductId
                ";

                await connection.ExecuteAsync(updateProductSql, new { newRating, newNumOfReviews, review.ProductId });

                return true;

            }
            else
            {
                return false;
            }

        }

        public async Task<IEnumerable<Review>> GetReviews(int productId)
        {
            using var connection = _context.CreateConnection();
            var sql = @"
                SELECT * FROM Reviews
                WHERE ProductId = @productId
                ORDER BY Likes DESC, CreatedTime DESC
                
            ";
            return await connection.QueryAsync<Review>(sql, new { productId });
        }

        public async Task<bool> LikeReview(int userId, int reviewId)
        {
            using var connection = _context.CreateConnection();

            Console.WriteLine(userId);
            Console.WriteLine(reviewId);

            var getReviewSql = @"
                SELECT * FROM Reviews
                WHERE Id = @ReviewId
            ";

            var review = await connection.QuerySingleOrDefaultAsync<Review>(getReviewSql, new
            {
                ReviewId = reviewId
            });

            if (review != null)
            {
                var getUserSql = @"
                    SELECT * FROM Users
                    WHERE Id = @UserId
                ";
                var user = await connection.QuerySingleOrDefaultAsync<User>(getUserSql, new
                {
                    UserId = userId
                });

                if (user != null)
                {
                    var getReviewLikeSql = @"
                        SELECT * FROM ReviewLikes
                        WHERE UserId = @UserId
                        AND ReviewId = @ReviewId
                    ";
                    var likedReview = await connection.QuerySingleOrDefaultAsync<ReviewLike>(getReviewLikeSql, new
                    {
                        UserId = userId,
                        ReviewId = reviewId
                    });
                    if (likedReview != null)
                    {
                        return false;
                    }

                    var updateLikesSql = @"
                        UPDATE Reviews
                        SET Likes = Likes + 1
                        WHERE Id = @ReviewId;

                        INSERT INTO ReviewLikes (UserId, ReviewId)
                        VALUES (@UserId, @ReviewId);
                    ";
                    await connection.ExecuteAsync(updateLikesSql, new
                    {
                        ReviewId = reviewId,
                        UserId = userId
                    });

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}

