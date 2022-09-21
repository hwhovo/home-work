using YB.Todo.Core.Models;

namespace YB.Todo.Core.Interfaces.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoModel>> GetTodosAsync(string? description);
        Task<TodoModel> GetTodoAsync(long id);
        Task<TodoModel> AddTodoAsync(TodoRequestModel todo);
        Task<TodoModel> EditTodoAsync(TodoEditModel todoModel);
        Task DeleteAsync(long id);
    }
}
