using Infrastructure.IP.DependencyInjection.Infrastructure;
using IP.Application.Comands.Requests;
using IP.Services.Proxy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
//app.UseMiddleware<ProxyMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("GatewayHandler",
([FromServices] IMediator mediator, Request request) =>
{
    try
    {
        var result = mediator.Send(request).Result;
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
})
.WithName("GatewayHandler")
.WithOpenApi();

app.Run();