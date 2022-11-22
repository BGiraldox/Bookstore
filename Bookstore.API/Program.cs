using Bookstore.Core.Entities;
using Bookstore.Core.Interfaces.Common;
using Bookstore.Core.Interfaces.Persistence;
using Bookstore.Core.Services;
using Bookstore.Core.Utility.QueryHandler;
using Bookstore.Core.Validators;
using Bookstore.Infrastructure.Persistence.Repositories;
using FluentValidation;
using Microsoft.Azure.Cosmos;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// CosmosClient Instance
builder.Services.AddSingleton(options
    => new CosmosClient(connectionString: Environment.GetEnvironmentVariable("COSMOS_CONNECTION_STRING")));

//DI Repositories
builder.Services.AddScoped<IRepository<Author>, Repository<Author>>();
builder.Services.AddScoped<IRepository<Book>, Repository<Book>>();

//DI Services
builder.Services.AddScoped<IBaseService<Author>, BaseService<Author>>();
builder.Services.AddScoped<IBaseService<Book>, BaseService<Book>>();

//DI Validators
builder.Services.AddScoped<IValidator<QueryFilter<Author>>, QueryFilterValidator<Author>>();
builder.Services.AddScoped<IValidator<QueryFilter<Book>>, QueryFilterValidator<Book>>();

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
