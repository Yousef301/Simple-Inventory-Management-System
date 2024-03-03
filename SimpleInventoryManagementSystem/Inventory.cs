namespace SimpleInventoryManagementSystem;

public class Inventory
{
    public List<Product> Products { get; } = new();

    public void AddProduct(Product product)
    {
        if (product is null) throw new ArgumentNullException(nameof(product), "Product can't be null.");

        var index = GetProductIndexByName(product.Name);
        if (index != -1)
        {
            DisplayProductAlreadyExistsMessage(product.Name);
            return;
        }

        Products.Add(product);
        DisplayProductAddedSuccessfullyMessage(product.Name);
    }

    public void ViewAllProducts()
    {
        Console.Clear();
        if (Products.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        Console.WriteLine($"Inventory contains {Products.Count} item(s):");
        var itemCnt = 1;
        foreach (var product in Products)
        {
            Console.WriteLine($"Item {itemCnt}:");
            product.PrintProductDetails();
            Console.WriteLine();
            itemCnt++;
        }
    }

    public void DeleteProduct(int index) => Products.RemoveAt(index);

    public int GetProductIndexByName(string name) =>
        Products.FindIndex(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));

    private static void DisplayProductAlreadyExistsMessage(string productName) =>
        Console.WriteLine($"{productName} is already in the inventory.");

    private static void DisplayProductAddedSuccessfullyMessage(string productName) =>
        Console.WriteLine($"{productName} is added successfully.");
}