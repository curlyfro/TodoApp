namespace TodoApp.Data;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoApp.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; }
}