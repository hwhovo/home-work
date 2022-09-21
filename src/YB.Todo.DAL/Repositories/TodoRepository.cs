using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YB.Todo.Core.Entities;
using YB.Todo.Core.Interfaces.Repositores;

namespace YB.Todo.DAL.Repositories
{
    public class TodoRepository: ITodoRepository
    {
        private readonly DbContext _dbContext;
        public TodoRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TodoEntity todoEntity)
        {
            _dbContext.Entry(todoEntity).State = EntityState.Added;
        }

        public Task<TodoEntity?> FirstOrDefaultAsync(Expression<Func<TodoEntity, bool>> predicate)
        {
            return _dbContext.Set<TodoEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IList<TodoEntity>> FilterAsync(Expression<Func<TodoEntity, bool>> predicate, int skip = 0, int? take = null)
        {
            var dbSet = _dbContext.Set<TodoEntity>();

            var query = dbSet.Where(predicate).Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TodoEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TodoEntity>().ToListAsync();
        }

        public void Remove(TodoEntity todo)
        {
            _dbContext.Set<TodoEntity>().Remove(todo);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(TodoEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
