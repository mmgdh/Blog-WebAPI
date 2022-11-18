using CommonInitializer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TimeLineService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var curAssembly = Assembly.GetExecutingAssembly();
builder.Services.AddControllers();
builder.Services.AddDbContext<TimeLineDbContext>(option => option.UseSqlServer(Environment.GetEnvironmentVariable("DefaultDBConnStr") ?? builder.Configuration.GetValue<string>("ConnectionStrings:SqlServer")));

builder.ConifgureExtraService(new InitializerOptions
{
    EventBusQueueName = "TimeLineService",
    curAssembly = curAssembly
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseDefault();
app.MapControllers();

app.Run();
