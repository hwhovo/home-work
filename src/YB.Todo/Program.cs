using Microsoft.EntityFrameworkCore;
using YB.Todo.DAL.Context;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using YB.Todo.Middlewares;
using YB.Todo.Core.Interfaces.Services;
using YB.Todo.BLL.Services;
using YB.Todo.Core.Interfaces.Repositores;
using YB.Todo.DAL.Repositories;

const string localHostOrigins = "_localHostOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextPool<ApplicationDbContext>(
        options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("TodoDbConnection"), m => m.MigrationsAssembly("YB.Todo.DAL")));

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new CamelCaseNamingStrategy
        {
            OverrideSpecifiedNames = false
        }
    };

    options.SerializerSettings.Formatting = Formatting.Indented;
});

builder.Services.AddTransient<ITodoService, TodoService>();
builder.Services.AddTransient<ITodoRepository, TodoRepository>(x => new TodoRepository(x.GetRequiredService<ApplicationDbContext>()));

builder.Services.AddCors(opts => opts.AddPolicy(name: localHostOrigins, policy =>
{
    policy.WithOrigins("localhost:3000", "http://localhost:3000");
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(localHostOrigins);
}

app.UseExceptionHandler(ExceptionMiddleware.ExceptionHandler);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

