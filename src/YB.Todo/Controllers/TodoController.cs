using Microsoft.AspNetCore.Mvc;
using System.Net;
using YB.Todo.Core.Exceptions;
using YB.Todo.Core.Interfaces;
using YB.Todo.Core.Interfaces.Services;
using YB.Todo.Core.Models;

namespace YB.Todo.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<TodoModel>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? description)
    {
        var todos = await _todoService.GetTodosAsync(description);
        return new ApiResult<IEnumerable<TodoModel>>(todos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<TodoModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAsync([FromRoute]int id)
    {
        var todo = await _todoService.GetTodoAsync(id);

        return new ApiResult<TodoModel>(todo);
    }

    [HttpPost()]
    [ProducesResponseType(typeof(ApiResponse<TodoModel>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> AddAsync([FromBody] TodoRequestModel todo)
    {
        var addedTodo = await _todoService.AddTodoAsync(todo);

        return new ApiResult<TodoModel>(addedTodo);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<TodoModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] TodoRequestModel todo)
    {
        var editedTodo = await _todoService.EditTodoAsync(new TodoEditModel() { Id = id, Description = todo.Description });

        return new ApiResult<TodoModel>(editedTodo);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _todoService.DeleteAsync(id);

        return new ApiResult<object>(HttpStatusCode.NoContent);
    }

    [HttpPatch("{id}/status")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Status([FromRoute] int id, [FromQuery] bool isComplete)
    {
        await _todoService.EditTodoAsync(new TodoEditModel { Id = id, IsComplete = isComplete });

        return new ApiResult<object>(HttpStatusCode.NoContent);
    }
}