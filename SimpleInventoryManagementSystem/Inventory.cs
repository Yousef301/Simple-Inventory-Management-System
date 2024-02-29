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
        foreach (var product in _products)
        {
            product.ProductDetails();
            Console.WriteLine("-----------------------------\n");
        }
    }

    public void EditProduct(Product newProduct, int index)
    {
        if (index != -1) _products[index] = newProduct;
    }

    public int GetProductIndexByName(string name)
    {
        var index = _products.FindIndex(p => p.Name == name);
        return index;
    }
}