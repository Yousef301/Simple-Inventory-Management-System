using MongoDB.Driver;

namespace SimpleInventoryManagementSystem.Repositories;

public class ProductRepositoryMongoDbContext : IProductRepository
{
    private IMongoDatabase db;

    public ProductRepositoryMongoDbContext(string connectionString, string dbName)
    {
        var client = new MongoClient(connectionString);
        db = client.GetDatabase(dbName);
    }

    private IMongoCollection<Product> Products => db.GetCollection<Product>("products");

    public void AddProduct(Product product)
    {
        Products.InsertOne(product);
    }

    public bool DeleteProduct(string itemName)
    {
        var deleteResult = Products.DeleteOne(p => p.Name == itemName);
        return deleteResult.DeletedCount > 0;
    }

    public Product? GetProductByName(string itemName)
    {
        var projection = Builders<Product>.Projection.Exclude("_id");
        return Products.Find(p => p.Name == itemName).Project<Product>(projection).FirstOrDefault();
    }

    public void ViewAllProducts()
    {
        var projection = Builders<Product>.Projection.Exclude("_id");
        var productsWithoutId = Products.Find(_ => true).Project<Product>(projection).ToList();
        productsWithoutId.ForEach(p => p.GetProductDetails());
    }

    public List<Product>? GetAllProducts()
    {
        var projection = Builders<Product>.Projection.Exclude("_id");
        return Products.Find(_ => true).Project<Product>(projection).ToList();
    }

    public void UpdateProduct(string itemName, string propertyName, object newValue)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Name, itemName);
        UpdateDefinition<Product> update;

        switch (propertyName.ToLower())
        {
            case "name":
                update = Builders<Product>.Update.Set(p => p.Name, newValue);
                break;
            case "price":
                update = Builders<Product>.Update.Set(p => p.Price, newValue);
                break;
            case "quantity":
                update = Builders<Product>.Update.Set(p => p.Quantity, newValue);
                break;
            default:
                throw new ArgumentException("Invalid property name specified.");
        }

        Products.UpdateOne(filter, update);
    }

    public int GetNumberOfItems()
    {
        return (int)Products.CountDocuments(_ => true);
    }
}