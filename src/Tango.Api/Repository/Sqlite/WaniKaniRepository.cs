using Dapper;
using Tango.Api.Entities;

namespace Tango.Api.Repository.Sqlite;

public class WaniKaniRepository : IWaniKaniRepository
{
    public async Task<bool> CreateAsync(WaniKaniSource value)
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"INSERT INTO wanikani (val, subject_id, srs_stage) VALUES (@Value, @SubjectId, @SrsStage);", value);
        return result > 0;
    }

    public async Task<WaniKaniSource?> GetAsync(string key)
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<WaniKaniSource>(
            @"SELECT val Value, subject_id SubjectId, srs_stage SrsStage FROM wanikani WHERE val = @key LIMIT 1", new { key });
    }

    public async Task<bool> UpdateAsync(WaniKaniSource value)
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"UPDATE wanikani SET srs_stage = @SrsStage WHERE val = @Value", value);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(string key)
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"DELETE FROM wanikani WHERE val = @key", new { key });
        return result > 0;
    }
}