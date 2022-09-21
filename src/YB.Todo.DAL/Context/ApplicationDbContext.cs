using Microsoft.EntityFrameworkCore;
using YB.Todo.Core.Entities;
using YB.Todo.DAL.EntityConfigurations;

namespace YB.Todo.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<TodoEntity> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TodoEntityConfiguration());
        }
    }
}
