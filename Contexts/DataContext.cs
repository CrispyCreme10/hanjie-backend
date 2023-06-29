using System.Data;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Npgsql;

namespace Hanjie.Contexts;

public interface IDataContext
{
    IDbConnection CreateConnection();
}

public class MySqlDataContext : IDataContext
{
    private DbSettings _dbSettings;

    public MySqlDataContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = $"server={_dbSettings.Server};database={_dbSettings.Database};user={_dbSettings.User};password={_dbSettings.Password}";
        return new MySqlConnection(connectionString);
    }
}

public class PostgreSqlDataContext : IDataContext
{
    private DbSettings _dbSettings;

    public PostgreSqlDataContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = $"Host={_dbSettings.Server};Database={_dbSettings.Database};Username={_dbSettings.User};Password={_dbSettings.Password}";
        return new NpgsqlConnection(connectionString);
    }
}