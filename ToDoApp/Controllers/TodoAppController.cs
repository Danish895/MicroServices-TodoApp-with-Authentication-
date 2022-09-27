using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoApp.ToDoModel;
using ToDoApp.ToDoService;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            var url = "https://localhost:7190/api/UserRequest/Login";
            using var client = new HttpClient();
            var msg = new HttpRequestMessage(HttpMethod.Get, url);

            var res = await client.SendAsync(msg);

            var content = await res.Content.ReadAsStringAsync();

            //TodoModel StudentDetails = JsonContent.DeserializeObject<TodoModel>(content);
           


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
