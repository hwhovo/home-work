using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using YB.Todo.Core.Entities;
using YB.Todo.DAL.Context;
using YB.Todo.DAL.Repositories;
using Moq;

namespace YB.Todo.Test
{
    [TestFixture]
    public class TodoRespositoryTest
    {
        private DbContextOptions<ApplicationDbContext> _dbContextOptions;
        private TodoRepository _repository;
        private ApplicationDbContext _context;

        [OneTimeSetUp]
        public void Setup()
        {
            var dbName = $"PackageManagerDbContext_{DateTime.Now.ToFileTimeUtc()}";
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(dbName)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .Options;

            _context = new ApplicationDbContext(_dbContextOptions);
            _context.Add(new TodoEntity("testValue"));
            _context.SaveChanges();
            _repository = new TodoRepository(_context);
        }

        [Test]
        public async Task AddTodoTestAsync()
        {
            // Arrange
            var entity = new TodoEntity("testValue");
            
            // Act
            _repository.Add(entity);
            await _repository.SaveChangesAsync();

            // Assert
            var addedValue = await _repository.FirstOrDefaultAsync(x => x.Id == entity.Id);
            Assert.IsNotNull(addedValue);
            Assert.AreEqual(addedValue!.Description, entity.Description);
            Assert.Less(addedValue.CreatedDate.Date - addedValue.ModifiedDate, TimeSpan.FromSeconds(2));
            Assert.IsFalse(addedValue.IsComplete);
        }

        [Test]
        public async Task UpdateTodoTestAsync()
        {
            // Arrange
            var todoEntity = await _repository.FirstOrDefaultAsync(_ => true);
            todoEntity!.Description = "NewValue";
            todoEntity!.IsComplete = !todoEntity!.IsComplete;
            
            // Act
            _repository.Update(todoEntity);
            await _repository.SaveChangesAsync();

            // Assert
            var updatedValue = await _repository.FirstOrDefaultAsync(x => x.Id == todoEntity.Id);

            Assert.IsNotNull(updatedValue);
            Assert.AreEqual(updatedValue!.Description, todoEntity.Description);
            Assert.AreEqual(updatedValue.ModifiedDate, todoEntity.ModifiedDate);
            Assert.AreEqual(updatedValue.CreatedDate, todoEntity.CreatedDate);
            Assert.AreEqual(updatedValue.IsComplete, todoEntity.IsComplete);
        }

        [Test]
        public async Task DeleteTodoTestAsync()
        {
            // Arrange
            var todoEntity = await _repository.FirstOrDefaultAsync(_ => true);

            // Act
            _repository.Remove(todoEntity!);
            await _repository.SaveChangesAsync();

            // Assert
            var deletedValue = await _repository.FirstOrDefaultAsync(x => x.Id == todoEntity!.Id);

            Assert.IsNull(deletedValue);
        }
    }
}
