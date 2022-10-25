using Dapper;
using MySqlConnector;

namespace DapperLDemo;

class Label
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

    public Customer(string email, string name, string adressStreet, string adressZipcode, 
        string adressCity, string phoneNumber)
    {
        Email = email;
        Name = name;
        Adress_Street = adressStreet;
        Adress_Zipcode = adressZipcode;
        Adress_City = adressCity;
        Phone_Number = phoneNumber;
    }
};

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Learning dapper!");
        string? connectionString = "Server=localhost;Database=videoteket_tk;Uid=khaled;Pwd=khaled123;";
        string sqlCode;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            List<Label> labels = connection.Query<Label>("SELECT * FROM labels;").ToList();

            foreach (Label currentLabel in labels)
            {
                Console.WriteLine($"{currentLabel.Id}. {currentLabel.Name}");
            }

            Customer cust1 = CreateCustomer();
            
            sqlCode =
                     "INSERT INTO customers (email, name, adress_street, adress_zipcode, adress_city, phone_number) " +
                     "VALUES (@Email, @Name, @Adress_Street, @Adress_Zipcode, @Adress_City, @Phone_Number)";

            connection.Execute(sqlCode, cust1);
            
            // int result = connection.Execute(sqlCode, new
            // {
            //     cust1.Name,
            //     cust1.Email,
            //     cust1.Adress_Street,
            //     cust1.Adress_Zipcode,
            //     cust1.Adress_City,
            //     cust1.Phone_Number,
            // });
            
            Console.ReadLine();
        }
    }

    private static Customer CreateCustomer()
    {
        string tempEmail;
        string tempName;
        string tempAdressStreet;
        string tempAdressZip;
        string tempAdressCity;
        string tempPhoneNumber;
        
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
