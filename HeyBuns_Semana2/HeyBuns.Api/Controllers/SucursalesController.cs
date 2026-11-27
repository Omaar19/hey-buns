using HeyBuns.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeyBuns.Api.Controllers;

// Solo hay dos sucursales fijas, la lista es pública para que el cliente elija
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class SucursalesController : ControllerBase
{
    private readonly ISucursalService _sucursales;

    public SucursalesController(ISucursalService sucursales) => _sucursales = sucursales;

    [HttpGet]
    public async Task<IActionResult> Listar() => Ok(await _sucursales.ListarAsync());
}
