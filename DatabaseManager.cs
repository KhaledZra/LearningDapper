using Dapper;
using MySqlConnector;

namespace DapperLDemo;

class DatabaseManager
{
    private MySqlConnectionStringBuilder _connectionString;

    // , , 
    public DatabaseManager(string? password = "")
    {
        _connectionString = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            Database = "videoteket_tk",
            UserID = "pma",
            Password = password
        };
    }

    public MySqlConnection ConnectToDb()
    {
        return new MySqlConnection(_connectionString.ConnectionString);
    }

    public List<T> SqlSelect<T>(MySqlConnection db, string tableName) where T : IsItem
    {
        List<string> columnNames = new List<string>();
        string formatedColumnNames = "";
        
        foreach (var item in typeof(T).GetProperties())
        {
            columnNames.Add($"{tableName}.{item.ToString().Split(" ")[1]}");
        }
        formatedColumnNames = string.Join(", ", columnNames);
        
        //Console.WriteLine(formatedColumnNames);
        return db.Query<T>($"SELECT {formatedColumnNames} FROM {tableName};").ToList();
    }
}