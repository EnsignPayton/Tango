using Dapper;
using Tango.Api.Entities;

namespace Tango.Api.Repository.Sqlite;

public class WordRepository : IWordRepository
{
    public async Task<bool> CreateAsync(Word value)
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"INSERT INTO words (val, kf) VALUES (@Value, @KnowledgeFactor);", value);
        return result > 0;
    }

    public async Task<Word?> GetAsync(string key)
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Word>(
            @"SELECT val Value, kf KnowledgeFactor FROM words WHERE val = @key LIMIT 1", new { key });
    }

    public async Task<bool> UpdateAsync(Word value)
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"UPDATE words SET kf = @KnowledgeFactor WHERE val = @Value", value);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(string key)
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"DELETE FROM words WHERE val = @key", new { key });
        return result > 0;
    }

    public async Task<IEnumerable<Word>> GetAllAsync()
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        return await connection.QueryAsync<Word>(
            @"SELECT val Value, kf KnowledgeFactor FROM words");
    }

    public async Task<int> GetCountAsync(double minFactor)
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        return await connection.ExecuteScalarAsync<int>(
            @"SELECT COUNT(*) from words WHERE kf >= @minFactor", new { minFactor });
    }
}