using System.Data;
using Microsoft.Data.Sqlite;

namespace Tango.Api.Repository.Sqlite;

public static class ConnectionProvider
{
    public static async Task<IDbConnection> CreateDbConnectionAsync()
    {
        var connection = new SqliteConnection();
        connection.ConnectionString = "Data Source=./tango.sqlite";
        await connection.OpenAsync();
        return connection;
    }
}