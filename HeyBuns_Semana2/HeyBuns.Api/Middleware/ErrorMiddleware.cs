using HeyBuns.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HeyBuns.Api.Middleware;

// Atrapa las excepciones y responde siempre con un JSON { codigo, mensaje }
public class ErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorMiddleware> _logger;

    public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext contexto)
    {
        try
        {
            await _next(contexto);
        }
        catch (Exception ex)
        {
            var (codigo, mensaje) = ex switch
            {
                ReglaNegocioException => (400, ex.Message),
                NoEncontradoException => (404, ex.Message),
                CredencialesInvalidasException => (401, ex.Message),
                DbUpdateException => (400, "No se pudo guardar, revisa que los datos no estén repetidos o relacionados con otros registros"),
                _ => (500, "Error interno del servidor")
            };

            if (codigo == 500)
                _logger.LogError(ex, "Error no controlado");

            contexto.Response.StatusCode = codigo;
            await contexto.Response.WriteAsJsonAsync(new { codigo, mensaje });
        }
    }
}
