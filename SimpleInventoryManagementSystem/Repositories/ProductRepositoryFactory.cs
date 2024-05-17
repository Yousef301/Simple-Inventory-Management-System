using System.Data.SqlClient;
using SimpleInventoryManagementSystem.Helpers;

namespace SimpleInventoryManagementSystem.Repositories;

public class ProductRepositoryFactory
{
    public IProductRepositoryDb CreateProductRepository(string? repositoryType)
    {
        switch (repositoryType?.ToUpper())
        {
            case "MSSERVER":
                var connectionStringMs = GetConnectionString("MSServerConnectionStrings", "MS");

                return new ProductRepositoryMsServer(connectionStringMs);
            case "MONGODB":
                var connectionStringMongo = GetConnectionString("MongoDBConnectionStrings", "MONGO");

                return new ProductRepositoryMongoDbContext(connectionStringMongo);
            default:
                throw new ArgumentException($"{repositoryType} isn't available yet...");
        }
    }

    private string GetConnectionString(string key, string type)
    {
        var connectionSettings = AppSettingsManager.GetConnectionString(key);

        if (type.ToUpper() == "MS")
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = connectionSettings["DataSource"],
                InitialCatalog = connectionSettings["InitialCatalog"],
                IntegratedSecurity = connectionSettings["IntegratedSecurity"] == "True"
            };

            return connectionStringBuilder.ConnectionString;
        }
        else if (type.ToUpper() == "MONGO")
        {
            return connectionSettings["ConnectionString"] + ";" + connectionSettings["DatabaseName"];
        }

        return "";
    }
}