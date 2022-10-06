using CommonInitializer;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var CurAssembly = Assembly.GetExecutingAssembly();
// Add services to the container.
builder.ConifgureExtraService(new InitializerOptions
{
    EventBusQueueName = "SearchService",
    curAssembly = CurAssembly
});
builder.Services.AddControllers();
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
