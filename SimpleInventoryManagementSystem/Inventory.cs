namespace SimpleInventoryManagementSystem;

public class Inventory
{
    private List<Product> _products = new();

    public List<Product> Products
    {
        get { return _products; }
    }

    public void AddProduct(Product product)
    {
        int index = GetProductIndexByName(product.Name);
        if (index != -1) Console.WriteLine($"{product.Name} is already in the inventory.");
        else
        {
            _products.Add(product);
            Console.WriteLine($"{product.Name} is added successfully.");
        }
    }

    public void ViewAllProducts()
    {
        int itemCnt = 1;
        foreach (var product in _products)
        {
            Console.WriteLine($"Item {itemCnt}:");
            product.ProductDetails();
            Console.WriteLine();
            itemCnt++;
        }
    }
    
    public void DeleteProduct(int index) => _products.RemoveAt(index);

    public int GetProductIndexByName(string name)
    {
        var index = _products.FindIndex(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        return index;
    }
}