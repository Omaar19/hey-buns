using HeyBuns.Api.DTOs;
using HeyBuns.Api.Models;
using HeyBuns.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeyBuns.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _productos;

    public ProductosController(IProductoService productos) => _productos = productos;

    // Ver el menú es público
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Listar([FromQuery] int? categoriaId) =>
        Ok(await _productos.ListarAsync(categoriaId));

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Obtener(int id) =>
        Ok(await _productos.ObtenerAsync(id));

    // Editar el menú es solo del admin
    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Crear(ProductoDto dto)
    {
        var creado = await _productos.CrearAsync(dto);
        return CreatedAtAction(nameof(Obtener), new { id = creado.Id }, creado);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Actualizar(int id, ProductoDto dto) =>
        Ok(await _productos.ActualizarAsync(id, dto));

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _productos.EliminarAsync(id);
        return NoContent();
    }
}
