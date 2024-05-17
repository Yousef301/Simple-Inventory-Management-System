using System.Data.SqlClient;
using SimpleInventoryManagementSystem.Helpers;
using SimpleInventoryManagementSystem.Repositories;

namespace SimpleInventoryManagementSystem;

public class Program
{
    private static Inventory _inventory;

    public static void Main()
    {
        var databaseType = AppSettingsManager.GetDatabaseType("Database");

        var productRepositoryFactory = new ProductRepositoryFactory();

        var productRepository = productRepositoryFactory.CreateProductRepository(databaseType);

        _inventory = new Inventory(productRepository.GetAllProducts());

        string? choice;
        do
        {
            InventoryConsoleUI.MainMenu();
            ConsoleDisplay.MessageDisplay("\nEnter your selection: ", false);
            choice = ConsoleInputReader.ReadInput();

            switch (choice)
            {
                case "1":
                    ConsoleDisplay.ClearScreen();
                    var result = AddNewProductFromUserInput(productRepository);
                    ConsoleDisplay.SetForegroundColor(ConsoleColor.Green);
                    if (result.Item1) InventoryConsoleUI.DisplayProductAddedSuccessfullyMessage(result.Item2);
                    ConsoleDisplay.ResetColor();
                    break;
                case "2":
                    ConsoleDisplay.ClearScreen();

                    // _inventory.GetNumberOfItems() == 0
                    if (productRepository.GetNumberOfItems() == 0)
                    {
                        ConsoleDisplay.SetForegroundColor(ConsoleColor.DarkBlue);
                        InventoryConsoleUI.EmptyInventory();
                        ConsoleDisplay.ResetColor();
                        break;
                    }

                    // _inventory.ViewAllProducts();
                    var products = productRepository.GetAllProducts();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    for (int i = 0; i < products.Count; i++)
                    {
                        ConsoleDisplay.MessageDisplay($"\nProduct {i + 1} Details:");
                        products[i].GetProductDetails();
                    }

                    Console.ResetColor();


                    break;
                case "3":
                    ConsoleDisplay.ClearScreen();
                    ConsoleDisplay.MessageDisplay("\nEnter product name: ", false);

                    var productName = ConsoleInputReader.ReadInput();

                    UpdateProductByName(productName, productRepository);

                    break;
                case "4":
                    ConsoleDisplay.ClearScreen();
                    ConsoleDisplay.MessageDisplay("\nEnter product name: ", false);

                    var productToDeleteName = ConsoleInputReader.ReadInput();

                    if (_inventory.DeleteProduct(productToDeleteName))
                    {
                        productRepository.DeleteProduct(productToDeleteName);
                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Green);
                        ConsoleDisplay.MessageDisplay($"{productToDeleteName} deleted successfully.");
                        ConsoleDisplay.ResetColor();
                    }
                    else
                    {
                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
                        InventoryConsoleUI.ItemNotExist(productToDeleteName);
                        ConsoleDisplay.ResetColor();
                    }

                    break;
                case "5":
                    ConsoleDisplay.ClearScreen();
                    ConsoleDisplay.MessageDisplay("\nEnter product name: ", false);

                    var productToSearchName = ConsoleInputReader.ReadInput();

                    // var product = _inventory.GetProductByName(productToSearchName);
                    var product = productRepository.GetProductByName(productToSearchName);
                    if (product != null)
                    {
                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Cyan);
                        product.GetProductDetails();
                        ConsoleDisplay.ResetColor();
                    }
                    else
                    {
                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
                        InventoryConsoleUI.ItemNotExist(productToSearchName);
                        ConsoleDisplay.ResetColor();
                    }

                    break;
                case "6":
                    ConsoleDisplay.MessageDisplay("Exiting...");
                    break;
                default:
                    ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
                    ConsoleDisplay.MessageDisplay(
                        "Invalid input. Please enter 1, 2, 3, 4, 5 or 6 to select an option from the menu.");
                    ConsoleDisplay.ResetColor();
                    break;
            }
        } while (choice != "6");
    }

    private static (bool, string) AddNewProductFromUserInput(IProductRepository productRepository)
    {
        ConsoleDisplay.MessageDisplay("Enter details for the new product:");
        ConsoleDisplay.MessageDisplay("Name: ", false);
        var productName = ConsoleInputReader.ReadInput();

        ConsoleDisplay.MessageDisplay("Price: ", false);
        var productPrice = ConsoleInputReader.ReadInput();

        ConsoleDisplay.MessageDisplay("Quantity: ", false);
        var productQuantity = ConsoleInputReader.ReadInput();

        if (productName == null || productPrice == null || productQuantity == null)
        {
            ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
            ConsoleDisplay.MessageDisplay("Invalid input. Please provide non-null values for all fields.");
            ConsoleDisplay.ResetColor();
            return (false, "");
        }

        if (_inventory.GetProductByName(productName) != null)
        {
            ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
            InventoryConsoleUI.DisplayProductAlreadyExistsMessage(productName);
            ConsoleDisplay.ResetColor();
            return (false, "");
        }

        double parsedPrice;
        int parsedQuantity;
        if (!double.TryParse(productPrice, out parsedPrice) || !int.TryParse(productQuantity, out parsedQuantity))
        {
            ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
            ConsoleDisplay.MessageDisplay("Invalid input for price or quantity. Please provide valid numeric values.");
            ConsoleDisplay.ResetColor();
            return (false, "");
        }

        var product = new Product(productName, parsedPrice, parsedQuantity);

        if (!ValidationService.IsValidProduct(product))
        {
            ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
            ConsoleDisplay.MessageDisplay("Invalid input for price or quantity or name. Please provide valid values.");
            ConsoleDisplay.ResetColor();
            return (false, productName);
        }

        _inventory.AddProduct(product);
        productRepository.AddProduct(product);
        return (true, productName);
    }

    private static void UpdateProductByName(string name, IProductRepository productRepository)
    {
        var product = _inventory.GetProductByName(name);

        if (product == null)
        {
            InventoryConsoleUI.ItemNotExist(name);
            return;
        }

        InventoryConsoleUI.EditMenu();

        ConsoleDisplay.MessageDisplay("Enter your choice: ", false);
        var choice = ConsoleInputReader.ReadInput();

        switch (choice)
        {
            case "1":
                UpdateProductName(product, productRepository);
                break;
            case "2":
                UpdateProductPrice(product, productRepository);
                break;
            case "3":
                UpdateProductQuantity(product, productRepository);
                break;
            default:
                ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
                InventoryConsoleUI.InvalidInputMessage(
                    "Invalid input. Please enter 1, 2, or 3 to select an option from the menu.");
                ConsoleDisplay.ResetColor();
                break;
        }
    }

    private static void UpdateProductName(Product product, IProductRepository productRepository)
    {
        ConsoleDisplay.MessageDisplay("Enter new name: ", false);
        var newProductName = ConsoleInputReader.ReadInput();

        if (string.IsNullOrEmpty(newProductName))
        {
            ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
            ConsoleDisplay.MessageDisplay("Invalid product name. Please enter a non-empty name.");
            ConsoleDisplay.ResetColor();
            return;
        }

        if (!ValidationService.IsValidProductName(newProductName))
        {
            ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
            ConsoleDisplay.MessageDisplay(
                "Invalid product name. Please enter a name with a maximum of 30 characters.");
            ConsoleDisplay.ResetColor();
            return;
        }

        productRepository.UpdateProduct(product.Name, "name", newProductName);
        product.Name = newProductName;
        ConsoleDisplay.SetForegroundColor(ConsoleColor.Green);
        ConsoleDisplay.MessageDisplay("Name updated successfully.");
        ConsoleDisplay.ResetColor();
    }

    private static void UpdateProductPrice(Product product, IProductRepository productRepository)
    {
        ConsoleDisplay.MessageDisplay("Enter new price: ", false);
        var newProductPrice = ConsoleInputReader.ReadInput();

        if (double.TryParse(newProductPrice, out double newPrice) && ValidationService.IsValidProductPrice(newPrice))
        {
            productRepository.UpdateProduct(product.Name, "price", newProductPrice);
            product.Price = newPrice;
            ConsoleDisplay.SetForegroundColor(ConsoleColor.Green);
            ConsoleDisplay.MessageDisplay("Price updated successfully.");
            ConsoleDisplay.ResetColor();
        }
        else
        {
            ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
            ConsoleDisplay.MessageDisplay("Invalid price. Please enter a valid price value.");
            ConsoleDisplay.ResetColor();
        }
    }

    private static void UpdateProductQuantity(Product product, IProductRepository productRepository)
    {
        ConsoleDisplay.MessageDisplay("Enter new quantity: ", false);
        var newProductQuantity = ConsoleInputReader.ReadInput();

        if (int.TryParse(newProductQuantity, out int newQuantity) &&
            ValidationService.IsValidProductQuantity(newQuantity))
        {
            productRepository.UpdateProduct(product.Name, "quantity", newProductQuantity);
            product.Quantity = newQuantity;
            ConsoleDisplay.SetForegroundColor(ConsoleColor.Green);
            ConsoleDisplay.MessageDisplay("Quantity updated successfully.");
            ConsoleDisplay.ResetColor();
        }
        else
        {
            ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
            ConsoleDisplay.MessageDisplay("Invalid quantity. Please enter a valid quantity value.");
            ConsoleDisplay.ResetColor();
        }
    }
}