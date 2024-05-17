using Microsoft.Extensions.Configuration;

namespace SimpleInventoryManagementSystem.Helpers;

public class AppSettingsManager
{
    private static readonly IConfiguration _configuration;

    static AppSettingsManager()
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        while (!Directory.Exists(Path.Combine(currentDirectory, "SimpleInventoryManagementSystem")))
        {
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            if (currentDirectory == null)
            {
                throw new DirectoryNotFoundException("Could not find the project directory.");
            }
        }

        var builder = new ConfigurationBuilder()
            .SetBasePath(currentDirectory)
            .AddJsonFile("SimpleInventoryManagementSystem/appsettings.json", optional: true, reloadOnChange: true);
        _configuration = builder.Build();
    }

    public static Dictionary<string, string> GetConnectionString(string key)
    {
        var connectionStrings = _configuration.GetSection(key);
        var connectionStringDictionary = new Dictionary<string, string>();

        foreach (var child in connectionStrings.GetChildren())
        {
            connectionStringDictionary.Add(child.Key, child.Value);
        }

        return connectionStringDictionary;
    }

    public static string? GetDatabaseType(string key)
    {
        return _configuration[key];
    }
}