using ApiPersonajesAWS.Data;
using ApiPersonajesAWS.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(p => p.AddPolicy("corsenabled", options =>
{
    options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
string connectionString = builder.Configuration.GetConnectionString("MySql");
builder.Services.AddTransient<RepositoryPersonajes>();
builder.Services.AddDbContext<PersonajesContext>
    (options => options.UseMySQL(connectionString));

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapScalarApiReference(options =>
{
    options.Title = "Scalar Personajes";
    options.Theme = ScalarTheme.BluePlanet;
});

app.UseCors("corsenabled");

app.MapControllers();

app.Run();
