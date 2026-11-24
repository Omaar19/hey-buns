using HeyBuns.Api.DTOs;
using HeyBuns.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeyBuns.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SucursalesController : ControllerBase
{
    private readonly ISucursalService _sucursales;

    public SucursalesController(ISucursalService sucursales) => _sucursales = sucursales;

    [HttpGet]
    public async Task<IActionResult> Listar() => Ok(await _sucursales.ListarAsync());

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Crear(SucursalDto dto)
    {
        var creada = await _sucursales.CrearAsync(dto);
        return Created($"/api/sucursales/{creada.Id}", creada);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Actualizar(int id, SucursalDto dto) =>
        Ok(await _sucursales.ActualizarAsync(id, dto));
}
