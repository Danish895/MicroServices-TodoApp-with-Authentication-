using Microsoft.AspNetCore.Identity;

namespace ToDoApp.ToDoModel
{
    public class TodoModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Done { get; set; } 

        public string UserId { get; set; }
        //public IdentityUser User { get; set; }
    }
}
