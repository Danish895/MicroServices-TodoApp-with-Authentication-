using ToDoApp.DataAccessLayer.ToDoRepository;
using ToDoApp.ToDoModel;

namespace ToDoApp.ToDoService
{
    public class TodoAppService : ITodoAppService
    {
        private ITodoAppRepository _todoAppRepository;
        public TodoAppService(ITodoAppRepository todoAppRepository)
        {
            _todoAppRepository = todoAppRepository;
        }

        public async Task<IEnumerable<TodoModel>> GetAsync()
        {
            return await _todoAppRepository.GetAsync();
        }

        public async Task<TodoModel> PostAsync(TodoModel item)
        {
            return await _todoAppRepository.PostAsync(item);
            
        }
    }
}
