namespace DapperLDemo;

class Customer : IsItem
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
}