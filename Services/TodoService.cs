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
        return (await connection.QueryAsync<Todo>("SELECT * FROM todos")).ToList();
    }

    public async Task<Todo> GetTodoAsync(int id)
    {
        using var connection = _db.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Todo>("SELECT * FROM todos WHERE id = @Id", new { Id = id });
    }

    public async Task AddTodoAsync(Todo todo)
    {
        using var connection = _db.CreateConnection();
        var sql = "INSERT INTO todos (title, is_complete) VALUES (@Title, @IsComplete) RETURNING id";
        var id = await connection.ExecuteScalarAsync<int>(sql, todo);
        todo.Id = id;
    }

    public async Task UpdateTodoAsync(Todo todo)
    {
        using var connection = _db.CreateConnection();
        await connection.ExecuteAsync("UPDATE todos SET title = @Title, is_complete = @IsComplete WHERE id = @Id", todo);
    }

    public async Task DeleteTodoAsync(int id)
    {
        using var connection = _db.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM todos WHERE id = @Id", new { Id = id });
    }
}