namespace SimpleInventoryManagementSystem;

public class Menu
{
    public static void Run()
    {
        MainMenu();

        var choice = "0";
        while (Int32.Parse(choice) != 6)
        {
            Console.WriteLine("\nEnter your selection: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Hello");
                    break;
                default:
                    Console.WriteLine("Unavailable input...");
                    break;
            }
        }
    }

    private static void MainMenu()
    {
        Console.WriteLine("Simple Inventory Management System\n\nSelect From the Following:\n" +
                          "1. Add a product.\n2. View all products\n3. Edit a product.\n" +
                          "4. Delete a product.\n5. Search for a product.\n6. Exit.");
    }
}