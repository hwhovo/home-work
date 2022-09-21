using System.ComponentModel.DataAnnotations;

namespace YB.Todo.Core.Models
{
    public class TodoRequestModel
    {
        /// <summary>
        /// A short description of the task
        /// </summary>
        [Required]
        public string? Description { get; set; }
    }
}
