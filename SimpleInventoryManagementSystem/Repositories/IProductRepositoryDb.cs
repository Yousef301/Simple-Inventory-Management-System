namespace SimpleInventoryManagementSystem.Repositories;

public interface IProductRepositoryDb : IProductRepository
{
    public List<Product>? GetAllProducts();
}