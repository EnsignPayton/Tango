using Dapper;
using Tango.Api.Entities;

namespace Tango.Api.Repository.Sqlite;

public class CustomSourceRepository : ICustomSourceRepository
{
    public async Task<bool> CreateAsync(CustomSource value)
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"INSERT INTO custom (val, kf) VALUES (@Value, @KnowledgeFactor);", value);
        return result > 0;
    }

    public async Task<CustomSource?> GetAsync(string key)
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<CustomSource>(
            @"SELECT val Value, kf KnowledgeFactor FROM custom WHERE val = @key LIMIT 1", new { key });
    }

    public async Task<bool> UpdateAsync(CustomSource value)
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"UPDATE custom SET kf = @KnowledgeFactor WHERE val = @Value", value);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(string key)
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"DELETE FROM custom WHERE val = @key", new { key });
        return result > 0;
    }
}