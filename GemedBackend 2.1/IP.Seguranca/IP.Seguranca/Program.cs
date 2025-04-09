using Infrastructure.IP.DependencyInjection.Infrastructure;
using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Requests.Authorization;
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

app.MapPost("AutenticacaoHandler",
    ([FromServices] IMediator mediator, AuthenticationRequest request) =>
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
.WithName("AutenticacaoHandler")

.WithOpenApi();
app.MapPost("AutorizacaoHandler",
    ([FromServices] IMediator mediator, AuthorizationRequest request) =>
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
.WithName("AutorizacaoHandler")
.WithOpenApi();

app.UseHttpsRedirection();

app.Run();