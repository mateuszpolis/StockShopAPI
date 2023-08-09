using System;
using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace StockShopAPI.Helpers;

public class DataContext
{
    private DbSettings _dbSettings;

    public DataContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }

    public IDbConnection CreateConnection()
    {
        string connectionString = $"Host={_dbSettings.Server}; " +
            $"Database={_dbSettings.Database};" +
            $"Username={_dbSettings.UserId};" +
            $"Password={_dbSettings.Password};";

        return new NpgsqlConnection(connectionString);
    }

    public async Task Init()
    {
        await InitDatabase();
        await InitTables();
    }

    private async Task InitDatabase()
    {
        using var connection = CreateConnection();
        var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_dbSettings.Database}';";
        var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount);
        if (dbCount == 0)
        {
            var sql = $"CREATE DATABASE \"{_dbSettings.Database}\"";
            await connection.ExecuteAsync(sql);
        }
    }

    private async Task InitTables()
    {
        using var connection = CreateConnection();
        var sql = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id SERIAL PRIMARY KEY,               
                FirstName VARCHAR NOT NULL,
                LastName VARCHAR NOT NULL,
                Email VARCHAR NOT NULL,
                PasswordHash VARCHAR NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Products (
                Id SERIAL PRIMARY KEY,
                Name VARCHAR(255) NOT NULL,
                Brand VARCHAR(255) NOT NULL,
                Price FLOAT NOT NULL,
                Description TEXT NOT NULL,
                Discount INT NOT NULL,
                StockQuantity INT NOT NULL,
                Orders INT NOT NULL,
                CategoryId INT NOT NULL,
                Availability BOOLEAN NOT NULL,
                CreatedTime TIMESTAMP NOT NULL,
                UpdatedTime TIMESTAMP NOT NULL,
                Weight FLOAT,
                Dimensions VARCHAR(255),
                Rating INT
            );

            CREATE TABLE IF NOT EXISTS Categories (
                Id SERIAL PRIMARY KEY,
                Name VARCHAR(255) NOT NULL,
                Description VARCHAR(255),
                HasChildren BOOLEAN NOT NULL,
                ParentCategory INT REFERENCES Categories(Id) ON DELETE CASCADE,
                Transactions INT NOT NULL,
                Visits INT NOT NULL
            );
        ";

        await connection.ExecuteAsync(sql);
    }
}
