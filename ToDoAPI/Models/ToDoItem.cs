using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class ToDoItem
    {
        [Key]
        public int ToDoId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Note { get; set; }
        public bool IsComplete { get; set; }
        public DateTime? DeadLine { get; set; }

    }
}
