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
                FirstName VARCHAR,
                LastName VARCHAR,
                Email VARCHAR,
                PasswordHash VARCHAR
            );
        ";

        await connection.ExecuteAsync(sql);
    }
}
