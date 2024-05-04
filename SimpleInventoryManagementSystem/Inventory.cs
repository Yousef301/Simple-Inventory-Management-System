﻿namespace SimpleInventoryManagementSystem;

public class Inventory
{
    private List<Product> products;
    private int itemsCount;

    public Inventory()
    {
        products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
        itemsCount += 1;
    }

    public bool DeleteProduct(string itemName)
    {
        var product = products.FirstOrDefault(p => string.Equals(p.Name, itemName, StringComparison.OrdinalIgnoreCase));
        if (product != null)
        {
            products.Remove(product);
            itemsCount -= 1;
            return true;
        }

        return false;
    }

    public Product? GetProductByName(string itemName)
    {
        return products.FirstOrDefault(p => string.Equals(p.Name, itemName, StringComparison.OrdinalIgnoreCase));
    }

    public void ViewAllProducts()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        for (int i = 0; i < products.Count; i++)
        {
            ConsoleDisplay.MessageDisplay($"\nProduct {i + 1} Details:");
            products[i].GetProductDetails();
        }

        Console.ResetColor();
    }

    public int GetNumberOfItems() => itemsCount;
}