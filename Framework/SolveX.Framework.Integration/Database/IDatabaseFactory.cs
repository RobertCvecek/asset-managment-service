using System.Data;

namespace SolveX.Framework.Integration.Database;

public interface IDatabaseFactory
{
    /// <summary>
    /// Generates and returns new connection based on connection string passed in paramater
    /// </summary>
    /// <param name="connectionName">Key of connection string in configuration</param>
    /// <returns>Opened connection to database</returns>
    public IDbConnection GetConnection(string connectionName = "Default");
}
