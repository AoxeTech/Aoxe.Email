using Zaabee.Mongo;
using Zaabee.Mongo.Abstractions;
using Zaabee.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// auto register three tier
builder.Services.AddThreeTier();
// todo: config mongo
builder.Services.AddSingleton<IZaabeeMongoClient, ZaabeeMongoClient>();
// todo: config rabbitmq
builder.Services.AddSingleton<IZaabeeRabbitMqClient, ZaabeeRabbitMqClient>();


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