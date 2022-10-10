using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<ToDoTask> ToDoTasks { get; set; }
    public DbSet<Category> Categories { get; set; }
}