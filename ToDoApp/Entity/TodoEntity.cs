using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Entity
{
    public class TodoEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Done { get; set; } = false;
    }
}
