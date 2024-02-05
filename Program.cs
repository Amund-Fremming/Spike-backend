using Microsoft.EntityFrameworkCore;
using Repositories;
using Hubs;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<QuestionRepository>();
builder.Services.AddScoped<GameRepository>();
builder.Services.AddScoped<DeviceRepository>();
builder.Services.AddScoped<VoteRepository>();

builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<VoteService>();

builder.Services.AddSignalR();

// Connects to database
var databaseConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
builder.Services.AddDbContext<Data.AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");       // For testing locally
    options.UseNpgsql(connectionString);     // For testing locally
    
    // options.UseNpgsql(databaseConnectionString);
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

app.MapHub<GameHub>("/gamehub");

app.Run();
