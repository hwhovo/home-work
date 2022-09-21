using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YB.Todo.Core.Entities;

namespace YB.Todo.Core.Interfaces.Repositores
{
    public interface ITodoRepository
    {
        void Add(TodoEntity todo);
        Task SaveChangesAsync();
        void Remove(TodoEntity todo);
        void Update(TodoEntity todo);
        Task<IEnumerable<TodoEntity>> GetAllAsync();
        Task<IList<TodoEntity>> FilterAsync(Expression<Func<TodoEntity, bool>> predicate, int skip = 0, int? take = null, bool asNoTracking = false);
        Task<TodoEntity?> FirstOrDefaultAsync(Expression<Func<TodoEntity, bool>> predicate);
    }
}
