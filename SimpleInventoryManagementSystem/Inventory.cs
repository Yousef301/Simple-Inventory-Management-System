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
        var matches = _products.Where(p => String.Equals(p.Name, product.Name, StringComparison.CurrentCulture));
        if (matches.Any()) Console.WriteLine($"{product.Name} is already in the inventory.");
        else
        {
            _products.Add(product);
            Console.WriteLine($"{product.Name} is added successfully.");
        }
    }

    public void ViewAllProducts()
    {
        foreach (var product in _products)
        {
            product.ProductDetails();
            Console.WriteLine("-----------------------------\n");
        }
    }
    
}