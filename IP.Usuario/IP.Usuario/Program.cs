using Infrastructure.IP.DependencyInjection.Infrastructure;
using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Requests.Usuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.
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

#region Authentication
app.MapPost("Authentication",
    ([FromServices] IMediator mediator, AuthenticationRequest request) =>
    {
        try
        {
            var result = mediator.Send(request);
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    })
.WithName("Authentication")
.WithOpenApi();
#endregion

#region Usuario
app.MapPost("CreateUsuarioHandler",
    ([FromServices] IMediator mediator, UsuarioCreateRequest request) =>
{
    try
    {
        var result = mediator.Send(request);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
})
.WithName("CreateUsuarioHandler")
.WithOpenApi();

app.MapPost("GetByCPFHandler",
    ([FromServices] IMediator mediator, UsuarioGetByCPFRequest request) =>
{
    try
    {
        var result = mediator.Send(request);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
})
.WithName("GetByCPFHandler")
.WithOpenApi();

app.MapPost("GetByIdHandler",
    ([FromServices] IMediator mediator, UsuarioGetByIdRequest request) =>
    {
        try
        {
            var result = mediator.Send(request);
            return Results.Ok(7);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    })
.WithName("GetByIdHandler")
.WithOpenApi();

app.MapPost("SendInviteHandler",
    ([FromServices] IMediator mediator, UsuarioSendInviteRequest request) =>
    {
        try
        {
            var result = mediator.Send(request);
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    })
.WithName("SendInviteHandler")
.WithOpenApi();

app.MapPost("VincularClinicaHandler",
    ([FromServices] IMediator mediator, UsuarioVincularClinicaRequest request) =>
    {
        try
        {
            var result = mediator.Send(request);
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    })
.WithName("VincularClinicaHandler")
.WithOpenApi();
#endregion

app.Run();