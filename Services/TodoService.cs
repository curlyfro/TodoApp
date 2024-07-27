using Dapper;
using System.Data;
using TodoApp.Data;
using TodoApp.Models;

public class TodoService
{
    private readonly DatabaseConnection _db;

    public TodoService(DatabaseConnection db)
    {
        _db = db;
    }

    public async Task<List<Todo>> GetTodosAsync()
    {
        using var connection = _db.CreateConnection();
        return (await connection.QueryAsync<Todo>("SELECT * FROM Todos")).ToList();
    }

    public async Task<Todo> GetTodoAsync(int id)
    {
        using var connection = _db.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Todo>("SELECT * FROM Todos WHERE Id = @Id", new { Id = id });
    }

    public async Task AddTodoAsync(Todo todo)
    {
        using var connection = _db.CreateConnection();
        var sql = "INSERT INTO Todos (Title, IsComplete) VALUES (@Title, @IsComplete); SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = await connection.ExecuteScalarAsync<int>(sql, todo);
        todo.Id = id;
    }

    public async Task UpdateTodoAsync(Todo todo)
    {
        using var connection = _db.CreateConnection();
        await connection.ExecuteAsync("UPDATE Todos SET Title = @Title, IsComplete = @IsComplete WHERE Id = @Id", todo);
    }

    public async Task DeleteTodoAsync(int id)
    {
        using var connection = _db.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Todos WHERE Id = @Id", new { Id = id });
    }
}