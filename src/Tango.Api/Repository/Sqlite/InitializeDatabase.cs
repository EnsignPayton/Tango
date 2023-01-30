using Dapper;

namespace Tango.Api.Repository.Sqlite;

public static class InitializeDatabase
{
    public static async Task ExecuteAsync()
    {
        using var connection = await ConnectionProvider.CreateDbConnectionAsync();
        await connection.ExecuteAsync("""
CREATE TABLE IF NOT EXISTS words(
    val TEXT PRIMARY KEY,
    kf REAL NOT NULL
);
""");

        await connection.ExecuteAsync("""
CREATE TABLE IF NOT EXISTS custom(
    val TEXT PRIMARY KEY,
    kf REAL NOT NULL
);
""");

        await connection.ExecuteAsync("""
CREATE TABLE IF NOT EXISTS wanikani(
    val TEXT PRIMARY KEY,
    subject_id INTEGER NOT NULL,
    srs_stage INTEGER NOT NULL
);
""");
    }
}