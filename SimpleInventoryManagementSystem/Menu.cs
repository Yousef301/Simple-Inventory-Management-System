namespace SimpleInventoryManagementSystem;

public class Menu
{
    private static Inventory _inventory = new();

    public static void Run()
    {
        var choice = "0";
        while (Int32.Parse(choice) != 6)
        {
            MainMenu();
            Console.WriteLine("\nEnter your selection: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string name = ReadFromUser("name");
                    string priceStr = ReadFromUser("price");

                    double price = CheckIfDouble(priceStr);


                    string quantityStr = ReadFromUser("quantity");

                    int quantity = CheckIfInt(quantityStr);

                    Product product = new Product(name, price, quantity);

                    int index = _inventory.GetProductIndexByName(name);
                    if (index == -1)
                    {
                        Console.Clear();
                        _inventory.AddProduct(product);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"{name} is already in the inventory.");
                    }

                    break;

                case "2":
                    _inventory.ViewAllProducts();
                    break;

                case "3":
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
                        Console.WriteLine("Item isn't in the inventory.");
                    }

                    break;

                case "5":
                    SearchForProduct();
                    break;

                case "6":
                    break;

                default:
                    Console.WriteLine("Unavailable input...");
                    break;
            }
        }
    }

    private static string ReadFromUser(string attribute)
    {
        Console.Clear();
        Console.WriteLine($"Enter product {attribute}: ");
        string? value = Console.ReadLine();
        while (string.IsNullOrEmpty(value))
        {
            Console.Clear();
            Console.WriteLine($"Enter product {attribute}: ");
            value = Console.ReadLine();
        }

        return value;
    }

    private static void SearchAndEdit()
    {
        Console.Clear();
        string name = ReadFromUser("name");
        int index = _inventory.GetProductIndexByName(name);

        if (index != -1)
        {
        }
    }

    private static double CheckIfDouble(string value)
    {
        double price;
        while (true)
        {
            if (double.TryParse(value, out price))
            {
                break;
            }

            Console.WriteLine("Invalid input. Please enter a valid price.");
            value = ReadFromUser("price");
        }

        return price;
    }

    private static int CheckIfInt(string value)
    {
        int quantity;
        while (true)
        {
            if (int.TryParse(value, out quantity))
            {
                break;
            }

            Console.WriteLine("Invalid input. Please enter a valid quantity.");
            value = ReadFromUser("quantity");
        }

        return quantity;
    }

    private static void SearchForProduct()
    {
        Console.Clear();
        string name = ReadFromUser("name");
        int index = _inventory.GetProductIndexByName(name);
        if (index == -1) Console.WriteLine($"{name} isn't in the inventory.");
        else _inventory.Products[index].ProductDetails();
    }

    private static void MainMenu()
    {
        Console.WriteLine("Simple Inventory Management System\n\nSelect From the Following:\n" +
                          "1. Add a product.\n2. View all products\n3. Edit a product.\n" +
                          "4. Delete a product.\n5. Search for a product.\n6. Exit.");
    }
}