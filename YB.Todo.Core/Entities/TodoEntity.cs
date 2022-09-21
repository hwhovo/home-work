namespace YB.Todo.Core.Entities
{
    public class TodoEntity
    {
        public TodoEntity()
        {

        }

        public TodoEntity(string description)
        {
            Description = description;
            ModifiedDate = DateTime.Now;
            IsComplete = false;
        }

        public TodoEntity(bool isComplete)
        {
            ModifiedDate = DateTime.Now;
            IsComplete = isComplete;
        }

        /// <summary>
        /// An identifier of the task
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// A short description of the task
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// A flag that indicates the status of the task
        /// </summary>
        public bool IsComplete { get; set; }
        /// <summary>
        /// The date that a task has been created
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; } = DateTime.Now;
        /// <summary>
        /// The date in which the task has been modified last
        /// </summary>
        public DateTimeOffset ModifiedDate { get; set; }

    }
}