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

    public string GetFormatedSetString<T>(T data, string tableName) where T : IFormatedToString
    {
        string[] objectInputStrings = GetFormatedPropertiesString<T>().Split(", ");
        string[] objectPropertieStrings = GetFormatedPropertiesString<T>(tableName, true).Split(", ");
        string formatedSetString = "";

        for(int i = 0; i < objectInputStrings.Length; i++)
        {
            if (objectPropertieStrings.Length - 1 == i) // save last one diffrent
            {
                formatedSetString = formatedSetString + $"{objectPropertieStrings[i]} = {objectInputStrings[i]}";
            }
            else
            {
                formatedSetString = formatedSetString + $"{objectPropertieStrings[i]} = {objectInputStrings[i]}, ";
            }
        }

        return formatedSetString;
    }
    
    
    // CRUD methods
    // Create
    public void SqlInsert<T>(string tableName, T data) where T : IFormatedToString
    {
        string columnNames = GetFormatedPropertiesString<T>(tableName, true);
        string columnNamesValues = GetFormatedPropertiesString<T>();
        
        string sqlCode =
            $"INSERT INTO {tableName} ({columnNames}) " +
            $"VALUES ({columnNamesValues})";
        
        ConnectToDb().Execute(sqlCode, data);
    }
    
    // Read
    public List<T> SqlSelect<T>(string tableName) where T : Entity 
    {
        string columnNames = GetFormatedPropertiesString<T>(tableName, false);

        return ConnectToDb().Query<T>($"SELECT {columnNames} FROM {tableName};").ToList();
    }
    
    public List<T> SqlSelectWhere<T>(string tableName, int id) where T : Entity
    {
        string columnNames = GetFormatedPropertiesString<T>(tableName, false);
        
        return ConnectToDb().Query<T>($"SELECT {columnNames} FROM {tableName} WHERE {tableName}.id = {id};").ToList();
    }
    
    // Update
    public bool SqlUpdate<T>(string tableName, T newData, int currentId) where T : Entity, IFormatedToString
    {
        string setString = GetFormatedSetString(newData, tableName);

        string sqlCode =
            @$"UPDATE {tableName} SET {setString} WHERE {tableName}.id = {currentId};";

        ConnectToDb().Execute(sqlCode, newData);

        return true;
    }
    
    // Delete
    public bool SqlDelete<T>(string tableName, T newData) where T : Entity => 
        ConnectToDb().Execute($"DELETE FROM {tableName} WHERE {tableName}.id = {newData.Id};") >= 1 ? true : false;
}