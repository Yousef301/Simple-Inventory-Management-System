using System.ComponentModel.DataAnnotations;

namespace SimpleInventoryManagementSystem;

public static class ValidationService
{
    public static bool IsValidProduct(Product product)
    {
        var validationContext = new ValidationContext(product);
        var validationResults = new List<ValidationResult>();
        return Validator.TryValidateObject(product, validationContext, validationResults, true);
    }

    public static bool IsValidProductName(string productName)
    {
        var product = new Product(productName, 0.01, 0);
        return IsValidProduct(product);
    }

    public static bool IsValidProductPrice(double productPrice)
    {
        var product = new Product("test", productPrice, 0);
        return IsValidProduct(product);
    }

    public static bool IsValidProductQuantity(int productQuantity)
    {
        var product = new Product("test", 0.1, productQuantity);
        return IsValidProduct(product);
    }
}