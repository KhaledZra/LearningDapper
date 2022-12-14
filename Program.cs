using Dapper;
using MySqlConnector;

namespace DapperLDemo;

#region Menu State enum

enum MenuState // TODO Need to add more options later maybe?
{
    MainMenu,
    
    CreateMenu,
    ReadMenu,
    UpdateMenu,
    DeleteMenu,
    
    Exit
}

#endregion

class Program
{
    private static MenuState _menuState = MenuState.MainMenu;
    // Creating DB handler Object
    private static DatabaseManager _dbManager = new DatabaseManager();
    
    public static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Learning dapper!");

        Console.WriteLine(_dbManager.SqlUpdate("customers", CreateCustomer(), _dbManager.SqlSelectWhere<Customer>("customers", 0)[0]));


        //while (MenuHandler() != 0) {}
    }

    public static List<string> ListMethod()
    {
        List<string> strings = new List<string>();
        
        strings.Add("test");
        strings.Add("test");
        strings.Add("test");

        return strings;
        
        // List<string> listTest = ListMethod();
        // IEnumerable<string> ieTest = IEnumerableMethod();
        //
        // Console.WriteLine(listTest);
        // Console.WriteLine(ieTest);
    }
    
    public static IEnumerable<string> IEnumerableMethod()
    {
        List<string> strings = new List<string>();
        
        strings.Add("test");
        strings.Add("test");
        strings.Add("test");

        return strings;
        
        // Displays the values of the Array.
        // int i = 0;
        // System.Collections.IEnumerator myEnumerator = ieTest.GetEnumerator();
        // Console.WriteLine( "The Array contains the following values:" );
        // while (( myEnumerator.MoveNext() ) && ( myEnumerator.Current != null ))
        //     Console.WriteLine( "[{0}] {1}", i++, myEnumerator.Current );
    }


    #region GUI

    private static void VisualHeader(string headerName)
    {
        Console.WriteLine("+-------------------------------+");
        Console.WriteLine("             "+ headerName + "              ");
        Console.WriteLine("+-------------------------------+");
    }

    #endregion
    

    #region Input Handlers

    private static int ForceInteger(string prompt)
    {
        int result;
        bool isParsed = false;
        bool isAddedErrorMsg = false;
        
        do
        {
            Console.Write(prompt);
            isParsed = int.TryParse(Console.ReadLine(), out result);
            if (isParsed == false)
            {
                ClearLastLine();
                if (isAddedErrorMsg == false)
                {
                    prompt = "Only numbers! " + prompt;
                    isAddedErrorMsg = true;
                }
            }
        } while (isParsed == false);

        return result;
    }

    private static void ClearLastLine()
    {
        // Clear last attempt and reset write spot to original
        Console.SetCursorPosition(0, Console.CursorTop -1);
        Console.Write(new string(' ', Console.BufferWidth));
        Console.SetCursorPosition(0, Console.CursorTop -1);
    }

    #endregion
    

    #region Menu Handlers

    private static void MainMenuSwitcher(int switchOn)
    {
        Console.Clear();
        
        // Early Return
        if (switchOn == 0)
        {
            Console.WriteLine("Bye!!");
            _menuState = MenuState.Exit;
            return;
        }
        
        if (switchOn == 1)
        {
            Console.WriteLine("Select Table to add data into!");
            _menuState = MenuState.CreateMenu;
        }
        else if (switchOn == 2)
        {
            Console.WriteLine("Select Table to check stored data!");
            _menuState = MenuState.ReadMenu;
        }
        else if (switchOn == 3)
        {
            Console.WriteLine("Select Table to update a specific row of data from!");
            _menuState = MenuState.UpdateMenu;
        }
        else if (switchOn == 4)
        {
            Console.WriteLine("Select Table to delete a specific row of data from!");
            _menuState = MenuState.DeleteMenu;
        }
        else if (switchOn == 5)
        {
            Console.WriteLine("Feature not implemented!");
        }
        else if (switchOn == 6)
        {
            Console.WriteLine("Feature not implemented!");
        }
        else if (switchOn == 7)
        {
            Console.WriteLine("Feature not implemented!");
        }
        else if (switchOn == 8)
        {
            Console.WriteLine("Feature not implemented!");
        }
        else if (switchOn == 9)
        {
            Console.WriteLine("Feature not implemented!");
        }
        else
        {
            Console.WriteLine("Error try again!");
        }
    }
    
    private static int MenuHandler()
    {
        int returnValue = 1;
        
        Console.WriteLine("---------");
        if (_menuState == MenuState.Exit)
        {
            returnValue = 0;
        }
        else if (_menuState == MenuState.MainMenu)
        {
            MainMenuSwitcher(MainMenu());
        }
        else
        {
            SelectMenuHandler(SelectMenu());
        }
        
        return returnValue;
    }
    
    private static void SelectMenuHandler(int choice)
    {
        Console.Clear();
        if (choice == 0)
        {
            _menuState = MenuState.MainMenu;
        }
        else if (choice >= 1 && choice <= 6)
        {
            if (_menuState == MenuState.CreateMenu)
            {
                CreateMenu(choice);
            }
            else if (_menuState == MenuState.ReadMenu)
            {
                ReadMenu(choice);
            }
            else if (_menuState == MenuState.UpdateMenu)
            {
                UpdateMenu();
            }
            else if (_menuState == MenuState.DeleteMenu)
            {
                DeleteMenu();
            }
            _menuState = MenuState.MainMenu;
        }
        else
        {
            Console.WriteLine("Try again!");
        }
    }

    #endregion

    
    #region Menu prints

    // Menu prints
    private static int MainMenu()
    {
        // C.R.U.D System model
        Console.WriteLine("1. Create row to table");
        Console.WriteLine("2. Read table");
        Console.WriteLine("3. Update row in table");
        Console.WriteLine("4. Delete row from table");
        Console.WriteLine("5. Search for customer");
        Console.WriteLine("6. Register Sale");
        Console.WriteLine("7. Register Loan");
        Console.WriteLine("8. Register Return");
        Console.WriteLine("9. Print receipt");
        Console.WriteLine("0. Exit");
        return ForceInteger("Choice: ");
    }

    private static int SelectMenu()
    {
        Console.WriteLine("1. Select Customers");
        Console.WriteLine("2. Select Medias");
        Console.WriteLine("3. Select Labels");
        Console.WriteLine("4. Select Orders");
        Console.WriteLine("5. Select Permanent items");
        Console.WriteLine("6. Select Temporary items");
        Console.WriteLine("0. Go back");
        return ForceInteger("Choice: ");
    }

    #endregion


    #region Menu result handlers - NOT DONE

    private static void CreateMenu(int choice) // TODO INSERT METHOD
    {
        if (choice == 1)
        {
            AddToTable<Customer>(_dbManager);
        }
        else if (choice == 5)
        {
            AddToTable<PermanentItem>(_dbManager);
        }
        else
        {
            Console.WriteLine("Not added feature!");
        }
    }
    
    private static void ReadMenu(int choice)
    {
        if (choice == 1)
        {
            ShowTable<Customer>(_dbManager);
        }
        else if (choice == 2)
        {
            ShowTable<Media>(_dbManager);
        }
        else if (choice == 3)
        {
            ShowTable<Label>(_dbManager);
        }
        else if (choice == 4)
        {
            Console.WriteLine("--Order--");
            Console.WriteLine("Not added feature!");
        }
        else if (choice == 5)
        {
            Console.WriteLine("--Permanent items--");
            Console.WriteLine("Not added feature!");
        }
        else if (choice == 6)
        {
            Console.WriteLine("--Temporary items--");
            Console.WriteLine("Not added feature!");
        }
    }  
    
    private static void UpdateMenu() // TODO UPDATE METHOD
    {
        Console.WriteLine("Not added feature!");
    }
    
    private static void DeleteMenu() // TODO DELETE METHOD
    {
        Console.WriteLine("Not added feature!");
    }

    #endregion


    #region Db and UI handlers NOT DONE

    // private static void InsertCustomer(MySqlConnection connection)
    // {
    //     VisualHeader("Inserting Customer");
    //     string sqlCode =
    //         "INSERT INTO customers (email, name, adress_street, adress_zipcode, adress_city, phone_number) " +
    //         "VALUES (@Email, @Name, @Adress_Street, @Adress_Zipcode, @Adress_City, @Phone_Number)";
    //
    //     connection.Execute(sqlCode, CreateCustomer());
    //
    //     // int result = connection.Execute(sqlCode, new
    //     // {
    //     //     cust1.Name,
    //     //     cust1.Email,
    //     //     cust1.Adress_Street,
    //     //     cust1.Adress_Zipcode,
    //     //     cust1.Adress_City,
    //     //     cust1.Phone_Number,
    //     // });
    // } // TODO REFACTOR AND REMOVE! InsertCustomer()

    private static void ShowTable<T>(DatabaseManager db) where T : Entity, IFormatedToString
    {
        string tableName = typeof(T).ToString().Split(".")[1];
        VisualHeader(tableName+"s");
        
        foreach (T currentItem in db.SqlSelect<T>(tableName+"s"))
        {
            Console.WriteLine($"{currentItem.Id}. {currentItem.FormatedToString()}");
            Console.WriteLine("---");
        }
    }

    private static void AddToTable<T>(DatabaseManager db) where T : IFormatedToString
    {
        string tableName = typeof(T).ToString().Split(".")[1] + "s";
        var newObject = CreateTableObject<T>();

        if (newObject != null)
        {
            db.SqlInsert<T>(tableName, newObject);
            Console.WriteLine(newObject.FormatedToString());
            Console.WriteLine("---Added!--");
        }
        else
        {
            Console.WriteLine("Failed!");
        }
    }

    private static dynamic CreateTableObject<T>()
    {
        if (typeof(T) == typeof(Customer))
        {
            return CreateCustomer();
        }
        
        if (typeof(T) == typeof(PermanentItem))
        {
            return CreatePermanentItem();
        }
        //
        // if (typeof(T) == typeof(Customer))
        // {
        //     return new object();
        // }
        //
        // if (typeof(T) == typeof(Customer))
        // {
        //     return new object();
        // }
        //
        // if (typeof(T) == typeof(Customer))
        // {
        //     return new object();
        // }
        
        // failed
        return null;
    }

    private static T FindRowInTable<T>(DatabaseManager db, int id) where T : Entity
    {
        string tableName = typeof(T).ToString().Split(".")[1] + "s";

        List<T> result = db.SqlSelectWhere<T>(tableName, id);

        if (result.Count == 0)
        {
            return null;
        }
        
        if (result.Count == 1)
        {
            return result[0];
        }
        
        Console.WriteLine("Security error!");
        return null;
    }

    private static void UpdateRowInTable<T>(DatabaseManager db, int id) where T : Entity
    {
        var currentData = FindRowInTable<Customer>(db, id);

        if (currentData == null)
        {
            Console.WriteLine("Not found!");
            return;
        }

        Console.WriteLine("Enter new information for to replace!");
        var newData = CreateTableObject<T>();
        
        // replace with sql method
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
        
        Console.Clear();

        return new Customer(tempEmail, tempName, tempAdressStreet, tempAdressZip,
            tempAdressCity, tempPhoneNumber);
    }
    
    private static PermanentItem CreatePermanentItem()
    {
        int label_Id;
        int media_Id;
        string? tempName;
        double tempPrice;


        
        Console.Write("Pick label: ");
        if (!int.TryParse(Console.ReadLine(), out label_Id))
        {
            Console.WriteLine("Only numbers!");
            return null;
        }
        else
        {
            // TODO create searchFeature that checks if the label_id given matches
            // TODO the labels stored in DB
        }
        
        Console.Write("Pick media: ");
        if (!int.TryParse(Console.ReadLine(), out media_Id))
        {
            Console.WriteLine("Only numbers!");
            return null;
        }
        else
        {
            // TODO create searchFeature that checks if the media_Id given matches
            // TODO the labels stored in DB
        }
        
        Console.Write("Enter product name: ");
        tempName = Console.ReadLine();
        
        Console.Write("Enter product price: ");
        if (double.TryParse(Console.ReadLine(), out tempPrice))
        {
            Console.WriteLine("Only numbers!");
            return null;
        }

        Console.Clear();

        return new PermanentItem();
    }

    #endregion
}
