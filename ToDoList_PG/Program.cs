using Microsoft.EntityFrameworkCore;
using ToDoList_PG.Mappers;
using ToDoList_PG.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<ToDoListDbContext>();
var connectionString = builder.Configuration.GetConnectionString("Conexao");

/*
builder.Services.AddDbContextPool<ToDoListDbContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
*/

builder.Services.AddDbContext<ToDoListDbContext>(options =>
{
    options.UseSqlite(connectionString);

});

builder.Services.AddAutoMapper(typeof(TarefaProfile).Assembly);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ToDoList_PG",
        Version = "v1",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Pedro Fellipe Medeiros Bittencourt",
        }
    });
    var xmlFile = "ToDoList_PG.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
