using Microsoft.EntityFrameworkCore;
using ToDoMS.WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("MyDb");
});



var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/todos/getall", (ApplicationDbContext context) =>
{
    var todos = context.Todos.ToList();
    return todos;
});

app.MapGet("/todos/create/{work}", async (string work, ApplicationDbContext context) =>
{
    context.Todos.Add(new ToDoMS.WebAPI.Models.Todo
    {
        Work = work,
    });
    await context.SaveChangesAsync();
    return new
    {
        Message = "\"Task is completed\""
    };
});

app.Run();
