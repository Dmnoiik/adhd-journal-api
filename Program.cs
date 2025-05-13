using AdhdJournalApi.Data;
using AdhdJournalApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Sqlite;
using SQLitePCL;


var builder = WebApplication.CreateBuilder(args);

Batteries.Init();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<AppDbContext>(
//    options => options.UseSqlServer(connectionString));

var dbPath = Path.Combine(Environment.CurrentDirectory, "adhd.db");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite("Data Source=adhd.db"));

builder.Services.AddScoped<IJournalEntryService, JournalEntryService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
        .WithOrigins("http://localhost:4200/")
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .AllowAnyHeader());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("AllowAngular");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
