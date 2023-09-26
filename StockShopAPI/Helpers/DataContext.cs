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

            CREATE TABLE IF NOT EXISTS Categories (
                Id SERIAL PRIMARY KEY,
                Name VARCHAR(255) NOT NULL,
                Description VARCHAR(255),
                HasChildren BOOLEAN DEFAULT false NOT NULL,
                ParentCategory INT REFERENCES Categories(Id) ON DELETE CASCADE,
                Transactions INT NOT NULL,
                Visits INT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Products (
                Id SERIAL PRIMARY KEY,
                Name VARCHAR(255) NOT NULL,
                Brand VARCHAR(255) NOT NULL,
                Price FLOAT NOT NULL,
                Description TEXT NOT NULL,
                Discount INT NOT NULL,
                StockQuantity INT NOT NULL,
                Orders INT DEFAULT 0 NOT NULL,
                CategoryId INT REFERENCES Categories(Id),
                Availability BOOLEAN NOT NULL,
                CreatedTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
                UpdatedTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
                Weight FLOAT,
                Dimensions VARCHAR(255),
                Rating FLOAT DEFAULT 0 NOT NULL,
                NumberOfReviews INT DEFAULT 0 NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Carts (
                Id SERIAL PRIMARY KEY,
                UserId INT REFERENCES Users(Id) ON DELETE CASCADE,
                TotalAmount FLOAT NOT NULL,
                TotalQuantity INT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS CartItems (
                Id SERIAL PRIMARY KEY,
                CartId INT REFERENCES Carts(Id) ON DELETE CASCADE,
                ProductId INT REFERENCES Products(Id) ON DELETE CASCADE,
                Quantity INT NOT NULL,
                TotalPrice FLOAT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Reviews (
                Id SERIAL PRIMARY KEY,
                UserId INT REFERENCES Users(Id) ON DELETE CASCADE,
                UserFirstName VARCHAR,
                UserLastName VARCHAR,
                ProductId INT REFERENCES Products(Id) ON DELETE CASCADE,
                Rating INT NOT NULL,
                ReviewText VARCHAR(255) NOT NULL,
                Likes INT DEFAULT 0 NOT NULL,
                OwnsProduct BOOLEAN DEFAULT false NOT NULL,
                CreatedTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL
            );

            CREATE TABLE IF NOT EXISTS ReviewLikes (
                UserId INT REFERENCES Users(Id) ON DELETE CASCADE,
                ReviewId INT REFERENCES Reviews(Id) ON DELETE CASCADE,
                LikeDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Images (
                Id SERIAL PRIMARY KEY,
                ProductId INT REFERENCES Products(Id) ON DELETE CASCADE,
                IsMainImage BOOLEAN DEFAULT false NOT NULL,
                AddedTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Parameters (
                Id SERIAL PRIMARY KEY,
                CategoryId INT REFERENCES Categories(Id),
                Name VARCHAR(50) NOT NULL,
                Description VARCHAR(255),
                ParameterType Varchar(10)
            );

            CREATE TABLE IF NOT EXISTS PredefinedChoices (
                Id SERIAL PRIMARY KEY,
                ParameterId INT REFERENCES Parameters(Id) ON DELETE CASCADE,
                Name VARCHAR(50),
                MinValue INT,
                MaxValue INT
            );

            CREATE TABLE IF NOT EXISTS ProductParameters (
                ProductId INT REFERENCES Products(Id) ON DELETE CASCADE,
                ParameterId INT REFERENCES Parameters(Id) ON DELETE CASCADE,
                ChoiceId INT REFERENCES PredefinedChoices(Id) ON DELETE CASCADE
            );
        ";

        await connection.ExecuteAsync(sql);
    }
}
