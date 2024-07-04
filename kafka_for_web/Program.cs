using System.Data.SqlClient;
using System.Text.Json.Serialization;
using Dapper;
using Npgsql;
using Kafka_for_web.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection 
builder.Services.AddDbContext<KafkaContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=webka;"));


// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
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

// allow cors 
app.UseCors();

app.MapControllers();

app.Run();
