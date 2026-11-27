using HeyBuns.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeyBuns.Api.Controllers;

// El menú lo puede ver cualquiera, incluso los clientes que piden sin cuenta
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categorias;

    public CategoriasController(ICategoriaService categorias) => _categorias = categorias;

    [HttpGet]
    public async Task<IActionResult> Listar() => Ok(await _categorias.ListarAsync());
}
