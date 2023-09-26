using System;
using System.Reflection.Metadata;
using StockShopAPI.Helpers;
using Dapper;
using StockShopAPI.Models;
using Parameter = StockShopAPI.Models.Parameter;
using Microsoft.IdentityModel.Tokens;

namespace StockShopAPI.Repositories
{
    public class ParameterRepository
    {
        private DataContext _context;
        public ParameterRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateParameter(Parameter parameter)
        {
            using var connection = _context.CreateConnection();
            var getCategorySql = @"
                SELECT * FROM Categories
                WHERE Id = @CategoryId
            ";
            var category = await connection.QuerySingleOrDefaultAsync<Category>(getCategorySql, new
            {
                CategoryId = parameter.CategoryId
            });

            if (category != null)
            {
                var insertParameterSql = @"
                    INSERT INTO Parameters (CategoryId, Name, Description, ParameterType)
                    VALUES (@CategoryId, @Name, @Description, @ParameterType)
                ";

                Console.WriteLine(parameter.Name);
                Console.WriteLine(parameter.Description);

                await connection.ExecuteAsync(insertParameterSql, new
                {
                    CategoryId = parameter.CategoryId,
                    Name = parameter.Name,
                    Description = parameter.Description,
                    ParameterType = parameter.ParameterType
                });

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Parameter>> GetParameters(int categoryId)
        {
            using var connection = _context.CreateConnection();
            var getParametersSql = @"
				WITH RECURSIVE CategoryHierarchy AS (
                    SELECT
                        c.Id AS CategoryId,
                        c.Name AS CategoryName,
                        c.ParentCategory,
                        p.Id AS ParameterId,
                        p.Name AS ParameterName,
                        p.Description AS ParameterDescription,
                        p.ParameterType
                    FROM Categories c
                    LEFT JOIN Parameters p ON c.Id = p.CategoryId
                    WHERE c.Id = @categoryId

                    UNION ALL

                    SELECT
                        c.Id AS CategoryId,
                        c.Name AS CategoryName,
                        c.ParentCategory,
                        p.Id AS ParameterId,
                        p.Name AS ParameterName,
                        p.Description AS ParameterDescription,
                        p.ParameterType
                    FROM Categories c
                    INNER JOIN CategoryHierarchy ch ON c.Id = ch.ParentCategory
                    LEFT JOIN Parameters p ON c.Id = p.CategoryId
                ),
                ParameterPredefinedChoices AS (
                    SELECT
                        pc.ParameterId AS ParameterId,
                        pc.Id AS PredefinedChoiceId,
                        pc.Name AS PredefinedChoiceName,
                        pc.MinValue AS PredefinedChoiceMinValue,
                        pc.MaxValue AS PredefinedChoiceMaxValue
                    FROM PredefinedChoices pc
                )
                SELECT DISTINCT                   
                    ph.ParameterId AS Id,
                    ph.CategoryId,
                    ph.ParameterName AS Name,
                    ph.ParameterDescription AS Description,
                    ph.ParameterType,
                    CASE WHEN COUNT(pc.PredefinedChoiceId) > 0 THEN
                        JSONB_AGG (
                            JSON_BUILD_OBJECT(
                                'Id', pc.PredefinedChoiceId,
                                'ParameterId', pc.ParameterId,
                                'Name', pc.PredefinedChoiceName,
                                'MinValue', pc.PredefinedChoiceMinValue,
                                'MaxValue', pc.PredefinedChoiceMaxValue
                            )
                        )
                    ELSE
                        NULL
                    END AS PredefinedChoices
                FROM CategoryHierarchy ph
                LEFT JOIN ParameterPredefinedChoices pc ON ph.ParameterId = pc.ParameterId
                WHERE ph.ParameterId IS NOT NULL
                GROUP BY ph.ParameterId, ph.CategoryId, ph.ParameterName, ph.ParameterDescription, ph.ParameterType;
			";
            var parameters = await connection.QueryAsync<Parameter>(getParametersSql, new { categoryId });

            return parameters;
        }

        public async Task<bool> CreateOption(PredefinedChoice option)
        {
            using var connection = _context.CreateConnection();
            var getParameterSql = @"
                SELECT * FROM Parameters
                WHERE Id = @parameterId
            ";
            var parameter = await connection.QuerySingleOrDefaultAsync<Parameter>(getParameterSql, new
            {
                parameterId = option.ParameterId
            });

            if (parameter != null)
            {
                var insertOptionSql = @"
                    INSERT INTO PredefinedChoices (ParameterId, Name, MinValue, MaxValue)
                    VALUES (@ParameterId, @Name, @MinValue, @MaxValue)
                ";

                await connection.ExecuteAsync(insertOptionSql, new
                {
                    ParameterId = option.ParameterId,
                    Name = option.Name,
                    MinValue = option.MinValue,
                    MaxValue = option.MaxValue
                });

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<PredefinedChoice>> GetOptions(int parameterId)
        {
            using var connection = _context.CreateConnection();
            var getOpsionsSql = @"
                SELECT * FROM PredefinedChoices
                WHERE ParameterId = @parameterId
            ";

            return await connection.QueryAsync<PredefinedChoice>(getOpsionsSql, new { parameterId });
        }
    }
}

