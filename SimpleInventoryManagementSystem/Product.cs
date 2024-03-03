namespace SimpleInventoryManagementSystem;

public class Product
{
    private string _name;
    private double _price;
    private int _quantity;

    public string Name
    {
        get => _name;
        set => _name = TruncateString(value, 30);
    }

    public double Price
    {
        get => _price;
        set => _price = IsValid(value, _price, 0);
    }

    public int Quantity
    {
        get => _quantity;
        set => _quantity = IsValid(value, _quantity, 0);
    }
    
    public Product(string name, double price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
    
    public void PrintProductDetails()
    {
        Console.WriteLine($"Product: {Name}");
        Console.WriteLine($"Quantity: {Quantity}");
        Console.WriteLine($"Price: {Price:C}"); // I liked :C specifier.
    }
    
    private static string TruncateString(string value, int maxLength) =>
        value.Length > maxLength ? value[..maxLength] : value;

    private static T IsValid<T>(T newValue, T oldValue, T min) where T : IComparable<T> =>
        newValue.CompareTo(min) > 0 ? newValue : oldValue;
}