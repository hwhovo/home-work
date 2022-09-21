using Microsoft.Extensions.Logging;
using YB.Todo.Core;
using YB.Todo.Core.Entities;
using YB.Todo.Core.Exceptions;
using YB.Todo.Core.Interfaces.Repositores;
using YB.Todo.Core.Interfaces.Services;
using YB.Todo.Core.Models;

namespace YB.Todo.BLL.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ILogger<TodoService> _logger;

        public TodoService(ITodoRepository todoRepository, ILogger<TodoService> logger)
        {
            _todoRepository = todoRepository;
            _logger = logger;
        }

        public async Task<TodoModel> AddTodoAsync(TodoRequestModel todo)
        {
            if (string.IsNullOrEmpty(todo.Description))
            {
                throw new LogicException(ErrorCode.DESCRIPTION_IS_EMPTY, _logger);
            }

            var todoEntity = new TodoEntity(todo.Description);
            _todoRepository.Add(todoEntity);

            await _todoRepository.SaveChangesAsync();

            return new TodoModel(todoEntity);
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _todoRepository.FirstOrDefaultAsync(x => x.Id == id) 
                ?? throw new LogicException(ErrorCode.TODO_NOT_FOUND, _logger);
            
            _todoRepository.Remove(entity);
            await _todoRepository.SaveChangesAsync();
        }

        public async Task<TodoModel> EditTodoAsync(TodoEditModel todoModel)
        {
            var entity = await _todoRepository.FirstOrDefaultAsync(x => x.Id == todoModel.Id) 
                ?? throw new LogicException(ErrorCode.TODO_NOT_FOUND, _logger);

            if (string.IsNullOrEmpty(todoModel.Description) && !todoModel.IsComplete.HasValue)
            {
                throw new LogicException(ErrorCode.EDITABLE_DATA_IS_EMPTY);
            }

            if (!string.IsNullOrEmpty(todoModel.Description))
            {
                if (todoModel.IsComplete != null)
                {
                    throw new LogicException(ErrorCode.YOU_CAN_NOT_CHANGE_DESCRIPTION_AND_STATUS_SAME_TIME, _logger);
                }

                if (entity.IsComplete)
                {
                    throw new LogicException(ErrorCode.COMPLETED_TASK_IS_READ_ONLY, _logger);
                }

                entity.Description = todoModel.Description;
            }

            if (todoModel.IsComplete.HasValue)
            {
                entity.IsComplete = todoModel.IsComplete.Value;
            }

            entity.ModifiedDate = DateTime.Now;
            _todoRepository.Update(entity);
            await _todoRepository.SaveChangesAsync();

            return new TodoModel(entity);
        }

        public async Task<TodoModel> GetTodoAsync(long id)
        {
            var todo = await _todoRepository.FirstOrDefaultAsync(x => x.Id == id) 
                ?? throw new LogicException(ErrorCode.TODO_NOT_FOUND, _logger);

            return new TodoModel(todo);
        }

        public async Task<IEnumerable<TodoModel>> GetTodosAsync(string? description)
        {
            var todos = await _todoRepository.FilterAsync(x => string.IsNullOrEmpty(description) || x.Description.Contains(description));

            return todos.Select(x => new TodoModel(x));
        }
    }
}
