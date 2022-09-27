using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.DataAccessLayer.ToDoDbContext;
using ToDoApp.ToDoModel;

namespace ToDoApp.DataAccessLayer.ToDoRepository
{
    public class TodoAppRepository : ITodoAppRepository
    {

        private readonly TodoDbContext _context;

        public TodoAppRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoModel>> GetAsync()
        {
            return await _context.TodoModels.ToListAsync(); //.Where(t => t.UserId == userId)
        }

        public async Task<TodoModel> PostAsync(TodoModel item)
        {
            TodoModel todo = new TodoModel() { Title = item.Title, Description = item.Description, Done= item.Done, UserId = item.UserId};
            await _context.TodoModels.AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;
        }
    }
}
