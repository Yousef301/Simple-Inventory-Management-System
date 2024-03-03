namespace SimpleInventoryManagementSystem;

public class Inventory
{
    public List<Product> Products { get; } = new();

    public void AddProduct(Product product)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product), "Product can't be null.");
        }

        var index = GetProductIndexByName(product.Name);
        if (index != -1)
        {
            Log.DisplayProductAlreadyExistsMessage(product.Name);
            return;
        }

        Products.Add(product);
        Log.DisplayProductAddedSuccessfullyMessage(product.Name);
    }

    public void ViewAllProducts()
    {
        Console.Clear();
        if (Products.Count == 0)
        {
            Log.EmptyInventory();
            return;
        }

        Log.ItemsInInventory(Products.Count);
        var itemCnt = 1;
        foreach (var product in Products)
        {
            Console.WriteLine($"Item {itemCnt}:");
            product.PrintProductDetails();
            Console.WriteLine();
            itemCnt++;
        }
    }

    public void DeleteProduct(int index, string itemName)
    {
        if (index != -1)
        {
            Console.Clear();
            Products.RemoveAt(index);
            Console.WriteLine($"{itemName} removed from the inventory.");
        }
        else
        {
            Console.Clear();
            Log.ItemNotExist(itemName);
        }
    }

    public int GetProductIndexByName(string name)
    {
        return Products.FindIndex(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
    }
}