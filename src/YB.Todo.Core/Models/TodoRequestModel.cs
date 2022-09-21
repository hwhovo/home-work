namespace YB.Todo.Core.Models
{
    public class TodoEditModel
    {
        public long Id { get; set; }
        public string? Description { get; set; }
        public bool? IsComplete { get; set; }
    }
}
