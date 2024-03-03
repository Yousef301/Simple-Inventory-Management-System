namespace SimpleInventoryManagementSystem;

public class Menu
{
    private static Inventory _inventory = new();

    public static void Run()
    {
        var choice = "0";
        while (Int32.Parse(choice) != 6)
        {
            Console.WriteLine("======================================================");
            Log.PrintMainMenu();
            Console.WriteLine("\nEnter your selection: ");
            choice = Console.ReadLine();
            if (choice == "" || !int.TryParse(choice, out _)) choice = "0";

            switch (choice)
            {
                case "1":
                    var name = ReadFromUser("name");

                    var priceStr = ReadFromUser("price");
                    var price = CheckIfNumeric<double>(priceStr, "price");


                    var quantityStr = ReadFromUser("quantity");
                    var quantity = CheckIfNumeric<int>(quantityStr, "quantity");

                    var product = new Product(name, price, quantity);

                    var index = _inventory.GetProductIndexByName(name);
                    if (index == -1)
                    {
                        Console.Clear();
                        _inventory.AddProduct(product);
                    }
                    else
                    {
                        Console.Clear();
                        Log.ItemExist(name);
                    }

                    break;

                case "2":
                    Console.Clear();
                    _inventory.ViewAllProducts();
                    break;

                case "3":
                    SearchAndEdit();
                    break;

                case "4":
                    string nameDelete = ReadFromUser("name");
                    int indexDelete = _inventory.GetProductIndexByName(nameDelete);
                    if (indexDelete != -1)
                    {
                        Console.Clear();
                        _inventory.DeleteProduct(indexDelete);
                        Console.WriteLine("Item deleted successfully.");
                    }
                    else
                    {
                        Console.Clear();
                        Log.ItemNotExist(nameDelete);
                    }

                    break;

                case "5":
                    SearchForProduct();
                    break;

                case "6":
                    break;

                default:
                    Log.InvalidInputMessage(
                        "Invalid input. Please enter 1, 2, 3, 4, 5 or 6 to select an option from the menu.");
                    break;
            }
        }
    }

    private static void SearchForProduct()
    {
        Console.Clear();
        var name = ReadFromUser("name");
        var index = _inventory.GetProductIndexByName(name);
        if (index == -1) Log.ItemNotExist(name);
        else _inventory.Products[index].PrintProductDetails();
    }

    private static void SearchAndEdit()
    {
        Console.Clear();
        var name = ReadFromUser("name");
        var index = _inventory.GetProductIndexByName(name);

        if (index != -1)
        {
            Log.PrintEditMenu();

            Console.WriteLine("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var newName = ReadFromUser("name");
                    _inventory.Products[index] = new Product(newName, _inventory.Products[index].Price,
                        _inventory.Products[index].Quantity);

                    break;
                case "2":
                    var newPrice = ReadFromUser("price");
                    double newPriceDouble = CheckIfNumeric<double>(newPrice, "price");

                    _inventory.Products[index] = new Product(_inventory.Products[index].Name, newPriceDouble,
                        _inventory.Products[index].Quantity);

                    break;
                case "3":
                    var newQuantity = ReadFromUser("quantity");
                    var newQuantityInt = CheckIfNumeric<int>(newQuantity, "quantity");

                    _inventory.Products[index] = new Product(_inventory.Products[index].Name,
                        _inventory.Products[index].Price,
                        newQuantityInt);

                    break;
                default:
                    Log.InvalidInputMessage(
                        "Invalid input. Please enter 1, 2, or 3 to select an option from the menu.");
                    break;
            }
        }
        else
        {
            Console.Clear();
            Log.ItemNotExist(name);
        }
    }

    private static T CheckIfNumeric<T>(string value, string attribute)
    {
        do
        {
            if (typeof(T) == typeof(int))
            {
                if (int.TryParse(value, out var result)) return (T)(object)result;
            }
            else if (typeof(T) == typeof(double))
            {
                if (double.TryParse(value, out var result)) return (T)(object)result;
            }

            Log.InvalidInputMessage("Invalid input. Please try again.");
            value = ReadFromUser(attribute);
        } while (true);
    }

    private static string ReadFromUser(string attribute)
    {
        Console.Clear();
        string? value;

        do
        {
            Console.WriteLine($"Enter product {attribute}: ");
            value = Console.ReadLine();
        } while (string.IsNullOrEmpty(value));

        return value;
    }
}