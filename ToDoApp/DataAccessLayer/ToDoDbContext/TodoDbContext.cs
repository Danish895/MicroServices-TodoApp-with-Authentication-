using Microsoft.EntityFrameworkCore;
using ToDoApp.ToDoModel;

namespace ToDoApp.DataAccessLayer.ToDoDbContext
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {

        }
        public DbSet<TodoModel> TodoModels { get; set; }

    }
}
