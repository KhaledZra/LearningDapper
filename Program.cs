using Dapper;
using MySqlConnector;

namespace DapperLDemo;

class Label
{
    public int Id { get; set; }
    public string? Name { get; set; }
};

class Media
{
    public int Id { get; set; }
    public string? Name { get; set; }
};

class Customer
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Adress_Street { get; set; }
    public string? Adress_Zipcode { get; set; }
    public string? Adress_City { get; set; }
    public string? Phone_Number { get; set; }
    
    // Used to load a customer
    public Customer(int id, string? email, string? name, string? adress_Street, string? adress_Zipcode, 
        string? adress_City, string? phone_Number)
    {
        Id = id;
        Email = email;
        Name = name;
        Adress_Street = adress_Street;
        Adress_Zipcode = adress_Zipcode;
        Adress_City = adress_City;
        Phone_Number = phone_Number;
    }

    // Used to create new customer
    public Customer(string? email, string? name, string? adress_Street, string? adress_Zipcode, 
        string? adress_City, string? phone_Number)
    {
        Email = email;
        Name = name;
        Adress_Street = adress_Street;
        Adress_Zipcode = adress_Zipcode;
        Adress_City = adress_City;
        Phone_Number = phone_Number;
    }

    public override string ToString()
    {
        return $"Email: {Email}, Name: {Name}, " +
               $"\nAdress: {Adress_Street}, {Adress_Zipcode}, {Adress_City}," +
               $"\nPhone Number: {Phone_Number}";
    }
};

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Learning dapper!");
        string? connectionString = "Server=localhost;Database=videoteket_tk;Uid=pma;Pwd=;";
        string? sqlCode;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            SelectLabels(connection);
            
            SelectMedias(connection);
            
            InsertCustomer(connection);
            
            SelectCustomers(connection);

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
    }

    private static void InsertCustomer(MySqlConnection connection)
    {
        VisualHeader("Inserting Customer");
        string sqlCode =
            "INSERT INTO customers (email, name, adress_street, adress_zipcode, adress_city, phone_number) " +
            "VALUES (@Email, @Name, @Adress_Street, @Adress_Zipcode, @Adress_City, @Phone_Number)";

        int result = connection.Execute(sqlCode, CreateCustomer());
        Console.WriteLine(result);
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
