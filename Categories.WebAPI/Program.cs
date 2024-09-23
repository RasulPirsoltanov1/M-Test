using Categories.WebAPI;
using Categories.WebAPI.Context;
using Categories.WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    var connString = builder.Configuration.GetConnectionString("SqlServer");
    opt.UseSqlServer(connString);
});


var app = builder.Build();

app.MapGet("/categories/getall", async (ApplicationDbContext context, CancellationToken cancellationToken) =>
{
    return await context.Categories.ToListAsync(cancellationToken);
});
app.MapPost("/categories/create", async (CreateCategoryDTO categoryDTO, ApplicationDbContext context, CancellationToken cancellationToken) =>
{
    if (context.Categories.Any(x => x.Name == categoryDTO.Name))
    {
        return Results.BadRequest(new { Message = "Category already exists!" });
    }
    var category = new Category
    {
        Name = categoryDTO.Name,
    };
    await context.Categories.AddAsync(category);
    await context.SaveChangesAsync();
    return Results.Ok(new { Message = "Category created successfully!" });
});

using(var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}


app.Run();
