using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace SolveX.Framework.Integration.Database;

/// <summary>
/// Class that generates and opens connection strings to database
/// </summary>
/// NOTE: Becouse we only use PostgresSQL we don't need to specifiy it in the name of the class
public class DatabaseFactory : IDatabaseFactory
{
    private readonly IConfiguration _configuration;

    /// NOTE: I would add some kind of encryption for connection strings saved in appsettings and then inject service that could decrypt it
    public DatabaseFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc/>
    public IDbConnection GetConnection(string connectionName = "Default") => new SqliteConnection(_configuration.GetConnectionString(connectionName));
}
