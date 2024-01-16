using Microsoft.EntityFrameworkCore;
using Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<QuestionRepo>();

// Addds repository and service
// TODO

// Connects to database
var databaseConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
builder.Services.AddDbContext<Data.AppDbContext>(options =>
{
    // var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");       // For testing locally
    // options.UseNpgsql(connectionString);
    options.UseNpgsql(databaseConnectionString);
});

var app = builder.Build();

// Does not enable Swagger for production
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} else {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
