using Dapper;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;
using MinimalAPI.Repositories;

DefaultTypeMap.MatchNamesWithUnderscores = true;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<PosgreSQLConfig>(opt =>
//    opt.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton(builder.Services.AddDbContext<MySQLConfig>(options =>
    options.UseSqlServer("WebApiDatabase")));

builder.Services.AddScoped<ProfilesRepository>();
builder.Services.AddScoped<UsersRepository>();
builder.Services.AddControllers();

builder.Services.AddMemoryCache();

builder.Services.AddSwaggerGen();

// CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("Nueva Politica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
//--------------------------------------------


var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

//CORS
app.UseCors("Nueva Politica");
//------------------------------------------------
app.UseAuthorization();

app.MapControllers();

app.Run();