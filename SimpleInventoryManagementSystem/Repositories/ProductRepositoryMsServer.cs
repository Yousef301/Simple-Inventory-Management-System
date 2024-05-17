using System.Data.SqlClient;

namespace SimpleInventoryManagementSystem.Repositories;

public class ProductRepositoryMsServer : IProductRepositoryDb
{
    private readonly string _connectionString;

    public ProductRepositoryMsServer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void AddProduct(Product product)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Products (name, price, quantity) VALUES (@Name, @Price, @Quantity)";
        command.Parameters.AddWithValue("@name", product.Name);
        command.Parameters.AddWithValue("@price", product.Price);
        command.Parameters.AddWithValue("@quantity", product.Quantity);
        command.ExecuteNonQuery();
    }

    public bool DeleteProduct(string itemName)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Products WHERE name = @Name";
        command.Parameters.AddWithValue("@name", itemName);
        return command.ExecuteNonQuery() > 0;
    }

    public Product? GetProductByName(string itemName)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT name, price, quantity FROM Products WHERE Name = @Name";
        command.Parameters.AddWithValue("@name", itemName);

        using (var reader = command.ExecuteReader())
        {
            if (reader.Read())
            {
                return new Product(reader.GetString(0), (double)reader.GetDecimal(1), reader.GetInt32(2));
            }
        }

        return null;
    }

    public void ViewAllProducts()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT name, price, quantity FROM Products";

        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"Name: {reader.GetString(0)}");
                Console.WriteLine($"Price: {reader.GetDecimal(1)}");
                Console.WriteLine($"Quantity: {reader.GetInt32(2)}");
                Console.WriteLine();
            }
        }
    }

    public List<Product>? GetAllProducts()
    {
        var products = new List<Product>();

        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT name, price, quantity FROM Products";

        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                products.Add(new Product(reader.GetString(0), (double)reader.GetDecimal(1), reader.GetInt32(2)));
            }
        }

        return products;
    }

    public void UpdateProduct(string itemName, string propertyName, object newValue)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();

        switch (propertyName.ToLower())
        {
            case "name":
                command.CommandText = "UPDATE Products SET name = @Value WHERE name = @Name";
                break;
            case "price":
                command.CommandText = "UPDATE Products SET price = @Value WHERE name = @Name";
                break;
            case "quantity":
                command.CommandText = "UPDATE Products SET quantity = @Value WHERE name = @Name";
                break;
            default:
                throw new ArgumentException("Invalid property name specified.");
        }

        command.Parameters.AddWithValue("@Name", itemName);
        command.Parameters.AddWithValue("@Value", newValue);

        command.ExecuteNonQuery();
    }


    public int GetNumberOfItems()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Products";
        return (int)command.ExecuteScalar();
    }
}