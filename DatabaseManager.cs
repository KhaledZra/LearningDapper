using Dapper;
using MySqlConnector;

namespace DapperLDemo;

class DatabaseManager
{
    private MySqlConnectionStringBuilder _connectionString;
    
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

    // Gets properties from a class and turns them into SQL insert code format
    private string GetFormatedPropertiesString<T>() where T : IFormatedToString
    {
        List<string> columnNames = new List<string>();
        string formatedColumnNames = "";
        
        foreach (var item in typeof(T).GetProperties())
        {
            if (item.ToString().Split(" ")[1].ToLower() != "id")
            {
                columnNames.Add($"@{item.ToString().Split(" ")[1]}");
            }
        }
        formatedColumnNames = string.Join(", ", columnNames);
        
        return formatedColumnNames;
    }
    
    // Gets properties from a class and turns them into SQL select code format
    private string GetFormatedPropertiesString<T>(string tableName, bool isRemoveId)
    {
        List<string> columnNames = new List<string>();
        string formatedColumnNames = "";
        
        foreach (var item in typeof(T).GetProperties())
        {
            if (isRemoveId)
            {
                if (item.ToString().Split(" ")[1].ToLower() != "id")
                {
                    columnNames.Add($"{tableName}.{item.ToString().Split(" ")[1]}");
                }
            }
            else
            {
                columnNames.Add($"{tableName}.{item.ToString().Split(" ")[1]}");
            }
            
        }
        formatedColumnNames = string.Join(", ", columnNames);
        
        return formatedColumnNames;
    }
    
    
    // CRUD methods
    public void SqlInsert<T>(MySqlConnection db, string tableName, T data) where T : IFormatedToString
    {
        string columnNames = GetFormatedPropertiesString<T>(tableName, true);
        string columnNamesValues = GetFormatedPropertiesString<T>();
        
        string sqlCode =
            $"INSERT INTO {tableName} ({columnNames}) " +
            $"VALUES ({columnNamesValues})";
        
        db.Execute(sqlCode, data);
    }
    
    public List<T> SqlSelect<T>(MySqlConnection db, string tableName) where T : IsItem // Read
    {
        string columnNames = GetFormatedPropertiesString<T>(tableName, false);

        return db.Query<T>($"SELECT {columnNames} FROM {tableName};").ToList();
    }
}