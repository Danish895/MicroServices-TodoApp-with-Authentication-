using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using ToDoApp.ToDoModel;
using ToDoApp.ToDoService;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TodoAppController : ControllerBase
    {
        private ITodoAppService _todoAppService;
        
        public TodoAppController(ITodoAppService todoAppService)
        {
            _todoAppService = todoAppService;
        }

        

        [HttpGet]
        [Route("GetAllTodoList")]
        public async Task<IEnumerable<TodoModel>> GetAsyncTodo()
        {
            var keyValue = HttpContext.Items["UserId"];


            Console.WriteLine(keyValue);

            IEnumerable<TodoModel> todos = await _todoAppService.GetAsync();
            return todos;
        }

        [HttpPost]
        [Route("PostTodoList")]
        public async Task<IActionResult> Post( TodoModel item)
        {
            TodoModel todo = await _todoAppService.PostAsync(item);
            return CreatedAtAction("Post", new { todo.Id }, todo);
        }
    }
}
