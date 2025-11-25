using Microsoft.EntityFrameworkCore;
using joballo_api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework with MySQL
builder.Services.AddDbContext<JobContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
                     ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")));
});

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use controllers
app.MapControllers();

app.Run();