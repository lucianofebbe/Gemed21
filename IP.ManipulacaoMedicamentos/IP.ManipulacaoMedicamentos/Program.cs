using IP.Application.Comands.Requests.Cliente;
using IP.Application.Comands.Requests.Usuario;
using Infrastructure.IP.DependencyInjection.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("CreateUsuario",
    ([FromServices] IMediator mediator, CreateUsuarioRequest request) =>
    {
        var result = mediator.Send(request);
        return Results.Ok(result);
    })
.WithName("CreateUsuario")
.WithOpenApi();

