using ToDoApp.ToDoModel;

namespace ToDoApp.ToDoService
{
    public interface ITodoAppService
    {
        Task<IEnumerable<TodoModel>> GetAsync();
        Task<TodoModel> PostAsync(TodoModel item);
    }
}
