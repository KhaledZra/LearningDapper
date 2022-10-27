using MySqlConnector;

namespace DapperLDemo;

class DatabaseManager
{
    private MySqlConnectionStringBuilder _connectionString;

    public DatabaseManager(string? server, string? database, string? userId, string? password = "")
    {
        _connectionString = new MySqlConnectionStringBuilder()
        {
            Server = server,
            Database = database,
            UserID = userId,
            Password = password
        };
    }

    public MySqlConnection ConnectToDb()
    {
        return new MySqlConnection(_connectionString.ConnectionString);
    }
}