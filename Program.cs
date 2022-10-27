﻿using Dapper;
using MySqlConnector;

namespace DapperLDemo;

class Program
{
    private static DatabaseManager dbManager = new DatabaseManager("localhost", "videoteket_tk", "pma");
    public static void Main(string[] args)
    {
        Console.WriteLine("Learning dapper!");

        using (dbManager.ConnectToDb())
        {
            SelectLabels(dbManager.ConnectToDb());
            
            SelectMedias(dbManager.ConnectToDb());
            
            //InsertCustomer(connection);
            
            SelectCustomers(dbManager.ConnectToDb());
        }
    }

    private static void InsertCustomer(MySqlConnection connection)
    {
        VisualHeader("Inserting Customer");
        string sqlCode =
            "INSERT INTO customers (email, name, adress_street, adress_zipcode, adress_city, phone_number) " +
            "VALUES (@Email, @Name, @Adress_Street, @Adress_Zipcode, @Adress_City, @Phone_Number)";

        int result = connection.Execute(sqlCode, CreateCustomer());
        Console.WriteLine(result);
        
        // int result = connection.Execute(sqlCode, new
        // {
        //     cust1.Name,
        //     cust1.Email,
        //     cust1.Adress_Street,
        //     cust1.Adress_Zipcode,
        //     cust1.Adress_City,
        //     cust1.Phone_Number,
        // });
    }

    private static void VisualHeader(string headerName)
    {
        Console.WriteLine("+-------------------------------+");
        Console.WriteLine("             "+ headerName + "              ");
        Console.WriteLine("+-------------------------------+");
    }

    private static void SelectLabels(MySqlConnection database)
    {
        List<Label> labels = database.Query<Label>("SELECT * FROM labels;").ToList();

        VisualHeader("Labels");
        
        foreach (Label currentItem in labels)
        {
            Console.WriteLine($"{currentItem.Id}. {currentItem.Name}");
        }
    }
    
    private static void SelectMedias(MySqlConnection database)
    {
        List<Media> medias = database.Query<Media>("SELECT * FROM media;").ToList();
        
        VisualHeader("Medias");
            
        foreach (Media currentItem in medias)
        {
            Console.WriteLine($"{currentItem.Id}. {currentItem.Name}");
        }
    }
    
    private static void SelectCustomers(MySqlConnection database)
    {
        List<Customer> customers = database.Query<Customer>("SELECT * FROM customers;").ToList();

        VisualHeader("Customers");
        
        foreach (Customer currentItem in customers)
        {
            Console.WriteLine($"{currentItem.Id}. " + currentItem.ToString());
        }
    }

    private static Customer CreateCustomer()
    {
        string? tempEmail;
        string? tempName;
        string? tempAdressStreet;
        string? tempAdressZip;
        string? tempAdressCity;
        string? tempPhoneNumber;
        
        Console.Write("Enter email: ");
        tempEmail = Console.ReadLine();
        
        Console.Write("Enter name: ");
        tempName = Console.ReadLine();
        
        Console.Write("Enter street adress: ");
        tempAdressStreet = Console.ReadLine();
        
        Console.Write("Enter zip adress: ");
        tempAdressZip = Console.ReadLine();
        
        Console.Write("Enter city adress: ");
        tempAdressCity = Console.ReadLine();
        
        Console.Write("Enter phone number: ");
        tempPhoneNumber = Console.ReadLine();

        return new Customer(tempEmail, tempName, tempAdressStreet, tempAdressZip,
            tempAdressCity, tempPhoneNumber);
    }
}
