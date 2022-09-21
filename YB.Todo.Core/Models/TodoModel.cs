using YB.Todo.Core.Entities;

namespace YB.Todo.Core.Models
{
    public class TodoModel
    {
        public TodoModel()
        { }

        public TodoModel(TodoEntity entity)
        {
            Id = entity.Id;
            Description = entity.Description;
            IsComplete = entity.IsComplete;
            CreatedDate = entity.CreatedDate;
            ModifiedDate = entity.ModifiedDate;
        }


        /// <summary>
        /// An identifier of the task
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// A short description of the task
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// A flag that indicates the status of the task
        /// </summary>
        public bool IsComplete { get; set; }
        /// <summary>
        /// The date that a task has been created
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }
        /// <summary>
        /// The date in which the task has been modified last
        /// </summary>
        public DateTimeOffset ModifiedDate { get; set; }

    }
}