using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Services;

public class TodoService
{
    private readonly ApplicationDbContext _context;

    public TodoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Todo>> GetTodosAsync()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task AddTodoAsync(Todo todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTodoAsync(Todo todo)
    {
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTodoAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo != null)
        {
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<Todo> GetTodoAsync(int id)
    {
        return await _context.Todos.FindAsync(id);
    }
}