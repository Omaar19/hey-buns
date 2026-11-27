using System.Security.Claims;
using HeyBuns.Api.DTOs;
using HeyBuns.Api.Models;
using HeyBuns.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeyBuns.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = Roles.Admin)]
public class EmpleadosController : ControllerBase
{
    private readonly IEmpleadoService _empleados;

    public EmpleadosController(IEmpleadoService empleados) => _empleados = empleados;

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] int? sucursalId) =>
        Ok(await _empleados.ListarAsync(sucursalId));

    [HttpPost]
    public async Task<IActionResult> Crear(EmpleadoDto dto)
    {
        var creado = await _empleados.CrearAsync(dto);
        return Created($"/api/empleados/{creado.Id}", creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, EmpleadoDto dto) =>
        Ok(await _empleados.ActualizarAsync(id, dto));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var idActual = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _empleados.EliminarAsync(id, idActual);
        return NoContent();
    }
}
