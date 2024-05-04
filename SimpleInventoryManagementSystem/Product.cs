using System.ComponentModel.DataAnnotations;

namespace SimpleInventoryManagementSystem;

public class Product
{
    [StringLength(30, ErrorMessage = "The name must be a maximum of 30 characters.")]
    public string Name { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than 0")]
    public double Price { get; set; }


    [Range(0, int.MaxValue, ErrorMessage = "Value must be greater or equals to 0")]
    public int Quantity { get; set; }

    public Product(string name, double price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public void GetProductDetails()
    {
        Console.WriteLine($"Product name: {Name}");
        Console.WriteLine($"Quantity: {Quantity}");
        Console.WriteLine($"Price: {Price:C}");
    }
}