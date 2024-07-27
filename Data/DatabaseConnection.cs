using System.Data;
using Npgsql;

namespace TodoApp.Data;

public class DatabaseConnection
{
    private readonly IConfiguration _configuration;

    public DatabaseConnection(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
}