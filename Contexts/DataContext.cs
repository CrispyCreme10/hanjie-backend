using System.Data;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace Hanjie.Contexts;

public class DataContext
{
    private DbSettings _dbSettings;

    public DataContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = $"server={_dbSettings.Server};database={_dbSettings.Database};user={_dbSettings.User};password={_dbSettings.Password}";
        return new MySqlConnection(connectionString);
    }
}