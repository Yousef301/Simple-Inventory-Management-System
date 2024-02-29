namespace SimpleInventoryManagementSystem;

public class Product
{
    private string name;
    private double price;
    private int quantity;

    public string Name
    {
        get => name;
        set => name = value.Length > 30 ? value[..30] : value;
    }

    public double Price
    {
        get => price;
        set => price = value > 0 ? value : price;
    }

    public int Quantity
    {
        get => quantity;
        set => quantity = value > 0 ? value : quantity;
    }

    public Product(string name, double price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public void IncreaseQuantity(int amount)
    {
        if (amount > 0) Quantity += amount;
        else Console.WriteLine($"Couldn't add {amount}");
    }

    public void DecreaseQuantity(int amount)
    {
        if (Quantity - amount > 0) Quantity += amount;
        else Console.WriteLine($"Can't remove {amount}. Available quantity is {Quantity}");
    }
}