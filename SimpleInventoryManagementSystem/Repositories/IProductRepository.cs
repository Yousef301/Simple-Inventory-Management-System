namespace SimpleInventoryManagementSystem.Repositories;

public interface IProductRepository
{
    void AddProduct(Product product);
    bool DeleteProduct(string itemName);
    Product? GetProductByName(string itemName);
    void ViewAllProducts();
    void UpdateProduct(string itemName, string propertyName, object newValue);
    int GetNumberOfItems();
}