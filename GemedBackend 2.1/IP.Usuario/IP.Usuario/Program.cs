using Infrastructure.IP.DependencyInjection.Infrastructure;
using IP.Application.Comands.Requests.Menu;
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

#region Usuario
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

app.MapPost("CreateUsuarioHandler",
    ([FromServices] IMediator mediator, UsuarioCreateRequest request) =>
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
.WithName("CreateUsuarioHandler")
.WithOpenApi();

app.MapPost("GetByCPFHandler",
    ([FromServices] IMediator mediator, UsuarioGetByCPFRequest request) =>
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
.WithName("GetByCPFHandler")
.WithOpenApi();

app.MapPost("GetByIdHandler",
    ([FromServices] IMediator mediator, UsuarioGetByIdRequest request) =>
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
.WithName("GetByIdHandler")
.WithOpenApi();

app.MapPost("SendInviteHandler",
    ([FromServices] IMediator mediator, UsuarioSendInviteRequest request) =>
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
.WithName("SendInviteHandler")
.WithOpenApi();

app.MapPost("VincularClinicaHandler",
    ([FromServices] IMediator mediator, UsuarioVincularClinicaRequest request) =>
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
.WithName("VincularClinicaHandler")
.WithOpenApi();

app.MapPost("MenuHandler",
    ([FromServices] IMediator mediator, MenuRequest request) =>
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
.WithName("MenuHandler")
.WithOpenApi();
#endregion

app.Run();