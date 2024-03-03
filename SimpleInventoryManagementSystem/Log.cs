namespace SimpleInventoryManagementSystem;

public class Log
{
    public static void PrintMainMenu()
    {
        Console.WriteLine("Simple Inventory Management System\n");
        Console.WriteLine("Select from the following:");
        Console.WriteLine("1. Add a product.");
        Console.WriteLine("2. View all products.");
        Console.WriteLine("3. Edit a product.");
        Console.WriteLine("4. Delete a product.");
        Console.WriteLine("5. Search for a product.");
        Console.WriteLine("6. Exit.");
    }

    public static void PrintEditMenu()
    {
        Console.Clear();
        Console.WriteLine("Select from the following:");
        Console.WriteLine("1. Edit product name.");
        Console.WriteLine("2. Edit product price.");
        Console.WriteLine("3. Edit product quantity.");
    }

    public static void InvalidInputMessage(string message) => Console.WriteLine(message);

    public static void DisplayProductAlreadyExistsMessage(string productName) =>
        Console.WriteLine($"{productName} is already in the inventory.");

    public static void DisplayProductAddedSuccessfullyMessage(string productName) =>
        Console.WriteLine($"{productName} is added successfully.");

    public static void EmptyInventory() => Console.WriteLine("Inventory is empty.");

    public static void ItemsInInventory(int items) =>
        Console.WriteLine($"Inventory contains {items} item(s):");

    public static void ItemNotExist(string itemName) => Console.WriteLine($"{itemName} isn't in the inventory");
    public static void ItemExist(string itemName) => Console.WriteLine($"{itemName} is already in the inventory.");
}