using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using YB.Todo.Core.Entities;
using Moq;
using System.Linq;
using YB.Todo.BLL.Services;
using Microsoft.Extensions.Logging;
using YB.Todo.Core.Interfaces.Repositores;
using System.Linq.Expressions;
using System.Collections.Generic;
using YB.Todo.Core.Models;
using YB.Todo.Core.Exceptions;
using YB.Todo.Core;

namespace YB.Todo.Test
{
    [TestFixture]
    public class TodoServiceTest
    {
        private TodoService _service;
        private const string MockDescription = "testResult";

        [OneTimeSetUp]
        public void Setup()
        {
            var repository = new Mock<ITodoRepository>();
            repository.Setup(x => x.FilterAsync(It.IsAny<Expression<Func<TodoEntity, bool>>>(), 0, null))
                .Returns(Task.Run(() => (IList<TodoEntity>)new List<TodoEntity>() { new TodoEntity(MockDescription) }));
            repository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<TodoEntity, bool>>>()))
                .Returns(Task.Run(() => (TodoEntity?)new TodoEntity(MockDescription)));
            repository.Setup(x => x.Add(It.IsAny<TodoEntity>()));
            repository.Setup(x => x.Update(It.IsAny<TodoEntity>()));
            repository.Setup(x => x.Remove(It.IsAny<TodoEntity>()));
            repository.Setup(x => x.SaveChangesAsync());

            _service = new TodoService(repository.Object, Mock.Of<ILogger<TodoService>>());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(MockDescription)]
        public async Task AddTodoTestAsync(string description)
        {
            // Act
            var addedTodoTask = _service.AddTodoAsync(new TodoRequestModel() { Description = description });

            // Assert
            if (description == null || description.Length == 0)
            {
                Assert.ThrowsAsync<LogicException>(async () => await addedTodoTask, ErrorCode.DESCRIPTION_IS_EMPTY.ToString());
            }
            else
            {
                var addedValue = await addedTodoTask;
                Assert.IsNotNull(addedValue);
                Assert.Greater(addedValue!.Description!.Length, 0);
                Assert.Less(addedValue.CreatedDate.Date - addedValue.ModifiedDate, TimeSpan.FromSeconds(2));
                Assert.IsFalse(addedValue.IsComplete);
            }
        }

        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase(MockDescription, null)]
        [TestCase(MockDescription, true)]
        [TestCase(MockDescription, false)]
        [TestCase(null, false)]
        [TestCase("", true)]
        public async Task EditTodoTestAsync(string description, bool? isComplete)
        {
            // Act
            var editTodoTask = _service.EditTodoAsync(new TodoEditModel() { Description = description, IsComplete = isComplete });

            // Assert
            if (string.IsNullOrEmpty(description) && !isComplete.HasValue)
            {
                Assert.ThrowsAsync<LogicException>(async () => await editTodoTask, ErrorCode.EDITABLE_DATA_IS_EMPTY.ToString());
            }
            else if (!string.IsNullOrEmpty(description) && isComplete.HasValue)
            {
                Assert.ThrowsAsync<LogicException>(async () => await editTodoTask, ErrorCode.YOU_CAN_NOT_CHANGE_DESCRIPTION_AND_STATUS_SAME_TIME.ToString());
            } else
            {
                var editedValue = await editTodoTask;
                Assert.IsNotNull(editedValue);

                if (!string.IsNullOrEmpty(description))
                {
                    Assert.AreEqual(editedValue.Description, description);
                }

                if (isComplete.HasValue)
                {
                    Assert.AreEqual(editedValue.IsComplete, isComplete);
                }
                Assert.Greater(editedValue!.Description!.Length, 0);
            }
        }

        [Test]
        public void DeleteTest()
        {
            // Act
            var deltedTodoTask = _service.DeleteAsync(1);

            // Assert
           Assert.DoesNotThrowAsync(async () => await deltedTodoTask);
        }


        [Test]
        public async Task GetTodoAsyncTest()
        {
            // Act
            var todoTask = _service.GetTodoAsync(1);

            // Assert
            Assert.DoesNotThrowAsync(async () => await todoTask);
            var todoValue = await todoTask;
            Assert.IsNotNull(todoValue);
            Assert.AreEqual(todoValue.Description, MockDescription);
        }


        [TestCase("")]
        [TestCase(null)]
        [TestCase(MockDescription)]
        public async Task GetTodosAsyncTest(string description)
        {
            // Act
            var todosTask = _service.GetTodosAsync(description);

            // Assert
            Assert.DoesNotThrowAsync(async () => await todosTask);
            var todoValue = await todosTask;
            Assert.IsNotNull(todoValue);
            Assert.AreEqual(todoValue.Count(), 1);
            Assert.AreEqual(todoValue.FirstOrDefault()?.Description, MockDescription);
        }
    }
}
