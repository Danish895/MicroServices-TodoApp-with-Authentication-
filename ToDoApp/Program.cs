using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ToDoApp.DataAccessLayer.ToDoDbContext;
using ToDoApp.DataAccessLayer.ToDoRepository;
using ToDoApp.Middlewares;
using ToDoApp.ToDoService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<ITodoAppService, TodoAppService>();

builder.Services.AddScoped<ITodoAppRepository,TodoAppRepository>();


//builder.Services.AddControllers()
//                   .AddSessionStateTempDataProvider();

//builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.UseMiddleware<GetUserIdFromTokenMiddleware>();

//app.UseSession();

app.MapControllers();

app.Run();
