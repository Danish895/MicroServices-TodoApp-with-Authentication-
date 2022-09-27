using ToDoApp.ToDoModel;

namespace ToDoApp.DataAccessLayer.ToDoRepository
{
    public interface ITodoAppRepository
    {
        Task<IEnumerable<TodoModel>> GetAsync();
        Task<TodoModel> PostAsync(TodoModel item);
    }
}
