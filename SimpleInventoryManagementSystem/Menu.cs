﻿namespace SimpleInventoryManagementSystem;

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
            if (choice == "" || !int.TryParse(choice, out _))
            {
                choice = "0";
            }

            switch (choice)
            {
                case "1":
                    var name = ReadFromUser("name");

                    var priceStr = ReadFromUser("price");
                    var price = Validator.CheckIfNumeric<double>(priceStr, "price");


                    var quantityStr = ReadFromUser("quantity");
                    var quantity = Validator.CheckIfNumeric<int>(quantityStr, "quantity");

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
                    Console.Clear();
                    var (itemIndex, itemName) = SearchByName();

                    EditByIndex(itemIndex, itemName);
                    break;

                case "4":
                    var (itemIndexDelete, itemNameDelete) = SearchByName();

                    _inventory.DeleteProduct(itemIndexDelete, itemNameDelete);
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
        if (index == -1)
        {
            Log.ItemNotExist(name);
        }
        else
        {
            _inventory.Products[index].PrintProductDetails();
        }
    }

    private static void EditByIndex(int index, string itemName)
    {
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
                    double newPriceDouble = Validator.CheckIfNumeric<double>(newPrice, "price");

                    _inventory.Products[index] = new Product(_inventory.Products[index].Name, newPriceDouble,
                        _inventory.Products[index].Quantity);

                    break;
                case "3":
                    var newQuantity = ReadFromUser("quantity");
                    var newQuantityInt = Validator.CheckIfNumeric<int>(newQuantity, "quantity");

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
            Log.ItemNotExist(itemName);
        }
    }

    public static string ReadFromUser(string attribute)
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

    private static (int, string) SearchByName()
    {
        var name = ReadFromUser("name");
        return (_inventory.GetProductIndexByName(name), name);
    }
}